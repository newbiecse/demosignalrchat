using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using DemoSignalRChat.Models;
using DemoSignalRChat.DAL;
using Microsoft.Ajax.Utilities;
using DemoSignalRChat.ViewModels;
using System.Threading.Tasks;
using System.Net;
using NSoup.Nodes;
using NSoup.Select;
using DemoSignalRChat.Preview;
using System;
using System.Text;


namespace DemoSignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        #region members

        static List<UserChatViewModel> ConnectedUsers = new List<UserChatViewModel>();

        private IStatusMessageRepository _statusMessageRepository;

        ApplicationDbContext _db = new ApplicationDbContext();
        UserChatViewModel _curUser;
        List<string> _friendListId;
        List<UserChatViewModel> _friendListConnected = new List<UserChatViewModel>();

        #endregion

        /// <summary>
        /// des:    get current user in ConnectedUsers
        /// </summary>
        public void SetCurUser()
        {
            // find current
            this._curUser = ConnectedUsers.Find(u => u.ConnectionId == Context.ConnectionId);
        }

        /// <summary>
        /// des:    get User Chat when knew userId
        /// goal:   find connectionId of user retrieve in private message
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserChatViewModel GetUserById(string userId)
        {
             return ConnectedUsers.Find(u => u.UserId == userId);
        }
        
        /// <summary>
        /// des: find friend list of current user
        /// </summary>
        public void SetFriendIdList()
        {
            // get friend list of current user
            var listFriend_1 = from f in this._db.Friends
                               where f.FriendId == this._curUser.UserId
                               select f.UserId;

            var listFriend_2 = from f in this._db.Friends
                               where f.UserId == this._curUser.UserId
                               select f.FriendId;

            this._friendListId = listFriend_1.Union(listFriend_2).Distinct().ToList();
        }
       
        /// <summary>
        /// des:    get friend list ONLINE of current user
        /// </summary>
        public void SetFriendListOnline()
        {
            this._friendListConnected = (from u in ConnectedUsers
                                      where this._friendListId.Contains(u.UserId)
                                      select new UserChatViewModel { UserId = u.UserId, ConnectionId = u.ConnectionId }).ToList();
        }

        /// <summary>
        /// des:    invoke some nessecery method
        /// </summary>
        public void init()
        {
            this.SetCurUser();
            this.SetFriendIdList();
            this.SetFriendListOnline();
        }

        /// <summary>
        /// des:    connect chat hub
        /// </summary>
        /// <param name="userId">userId connect</param>
        public void Connect(string userId)
        {
            var curConnectionId = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == curConnectionId) == 0)
            {
                ConnectedUsers.Add(new UserChatViewModel { ConnectionId = curConnectionId, UserId = userId });

                this.init();

                var friendListId_Online = (from f in this._friendListConnected
                                           select f.UserId).ToList();

                // send to all except caller client
                Clients.AllExcept(curConnectionId).onNewUserConnected(userId);

                // send to caller
                Clients.Caller.onConnected(friendListId_Online);
            }
        }

        public override Task OnDisconnected()
        {
            var id = Context.ConnectionId;

            this.init();

            var friendListId_Online = (from f in this._friendListConnected
                                       select f.UserId).ToList();

            // send to all friend list online
            var friendListConnectionId = (from f in this._friendListConnected
                                           select f.ConnectionId).ToList();

            ConnectedUsers.Remove(ConnectedUsers.Where(u => u.ConnectionId == id).Single());

            Clients.Clients(friendListConnectionId).signOut(this._curUser.UserId);
            return base.OnDisconnected();
        }

        public void TurnOffChat()
        {
                var curConnectionId = Context.ConnectionId;

                this.init();

                var tFriendIdListOnline = (from f in this._friendListConnected
                                           select f.UserId).ToList();

                // send to caller
                Clients.Caller.onConnected(tFriendIdListOnline);

                // Broad cast message to friend list
                var fListConnectionId = (from f in this._friendListConnected
                                           select f.ConnectionId).ToList();

                Clients.Clients(fListConnectionId).offChat(this._curUser.UserId);
        }

        public void SendMessageToAll(string userName, string message)
        {
            this.init();

            // store message to database
            this._statusMessageRepository.InsertStatusMessage(new StatusMessage { UserId = this._curUser.UserId, Message = message });
            this._statusMessageRepository.Save();

            var friendListConnectionId = (from f in this._friendListConnected
                                     select f.ConnectionId).ToList();

            // Broad cast message to friend list
            friendListConnectionId.Add(Context.ConnectionId);


            string title = "";
            string description = "";
            string src = "";
            string url = "";

            var link = new Link(message) ;
            if(!string.IsNullOrEmpty(link.Url))
            {
                url = link.Url;
                using (var client = new WebClient())
                { 
                    //client.DownloadData
                    client.Encoding = Encoding.UTF8;
                    string html = client.DownloadString(url);
                    Document doc = NSoup.NSoupClient.Parse(html);

                    // get title
                    title = doc.Select("title").First.Text();               

                    // get description
                    if (doc.Select("meta[name=description]") != null)
                    {
                        if (doc.Select("meta[name=description]").First != null)
                        { 
                            description = doc.Select("meta[name=description]").First.Attr("content");
                            if (string.IsNullOrEmpty(description))
                            {
                                description = title;
                            }
                        }
                    }
                
                    // get image
                    Elements imgs = doc.Select("img");
                    List<Img> images = new List<Img>();
                    foreach (var i in imgs)
                    {
                        images.Add(new Img(i.Attr("height"), i.Attr("width"), i.Attr("src")));
                    }
                    src = Img.GetSrcLargetestImage(images);

                    if(string.IsNullOrEmpty(src))
                    {
                        var EleIcons = doc.Select("link[rel=icon]");
                        if(EleIcons.Count > 0)
                        {
                            src = EleIcons.First.Attr("href");
                        }
                    }
                }
            }

            Clients.Clients(friendListConnectionId).messageReceived(userName, src, title, description);
        }


        /// <summary>
        /// des:    sent message chat
        /// </summary>
        /// <param name="userRetrieved_Id"></param>
        /// <param name="message"></param>
        public void SendPrivateMessage(string userRetrieved_Id, string message)
        {
            this.init();

            var fromUser = this.GetUserById(userRetrieved_Id);

            // store message to database
            this._db.PrivateMessages.Add(new PrivateMessage { UserSent_Id = this._curUser.UserId, UserRetrieved_Id = userRetrieved_Id, Content = message });
            _db.SaveChanges();

            // friend online
            if (fromUser.ConnectionId != null)
            {
                Clients.Client(fromUser.ConnectionId).privateMessageReceived(this._curUser.UserId, message);
            }
        }

    }
}