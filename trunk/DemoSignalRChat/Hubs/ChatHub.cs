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

        ApplicationDbContext _dbContext;
        IFriendRepository _friendRepository;
        IChatRepository _chatRepository;
        IPrivateMessageRepository _privateMessageRepository;
        
        List<string> _friendListId;
        List<UserChatViewModel> _friendListConnected = new List<UserChatViewModel>();

        string _curConnectionId;
        UserChatViewModel _curUser;

        static List<UserChatViewModel> ConnectedUsers = new List<UserChatViewModel>();

        #endregion

        public void Init()
        {
            // init repository
            this._dbContext = new ApplicationDbContext();
            this._chatRepository = new ChatRepository(this._dbContext);
            this._friendRepository = new FriendRepository(this._dbContext);
            this._privateMessageRepository = new PrivateMessageRepository(this._dbContext);

            // get current connectionId
            this._curConnectionId = this.Context.ConnectionId;

            // get chatViewModel of User via connectionId
            this._curUser = this._chatRepository.GetUserByConnectionId(ConnectedUsers, this.Context.ConnectionId);

            // get friendListId
            this._friendListId = this._friendRepository.GetFriendListId(this._curUser.UserId).ToList();

            // get friendListOnline
            this._friendListConnected = this._chatRepository.GetFriendListOnline(ConnectedUsers, this._friendListId, this._curUser.UserId);
        }
        

        public void Connect(string userId)
        {
            var curConnectionId = Context.ConnectionId;

            //this.Init();

            // case: user not connected yet
            if(ConnectedUsers.Count(u => u.ConnectionId == curConnectionId) == 0)
            //if (!this._chatRepository.IsConnected(ConnectedUsers, curConnectionId))
            {
                // add new user connected
                var newUserConnected = new UserChatViewModel { ConnectionId = curConnectionId, UserId = userId };
                ConnectedUsers.Add(newUserConnected);

                // init fields
                this.Init();

                // send to all except caller client
                Clients.AllExcept(this._curConnectionId).onNewUserConnected(userId);

                // send to friend list online
                var friendListId_OnLine = this._chatRepository.GetFriendListId_Online(this._friendListConnected);
                Clients.Caller.onConnected(friendListId_OnLine);
            }
        }

        public override Task OnDisconnected()
        {
            this.Init();

            // get friend list id online
            var friendListId_OnLine = this._chatRepository.GetFriendListId_Online(this._friendListConnected);

            // send to all friend list online
            var friendList_connectionID = this._chatRepository.GetFriendList_ConnectionId(this._friendListConnected);

            // remove current user
            this._chatRepository.RemoveUserConnected(ConnectedUsers, this._curUser);

            // call client
            Clients.Clients(friendList_connectionID).signOut(this._curUser.UserId);
            return base.OnDisconnected();
        }

        public void TurnOffChat()
        {
                var curConnectionId = Context.ConnectionId;

                this.Init();

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
            this.Init();

            //// store message to database
            //this._statusMessageRepository.InsertStatusMessage(new StatusMessage { UserId = this._curUser.UserId, Message = message });
            //this._statusMessageRepository.Save();

            //var friendListConnectionId = (from f in this._friendListConnected
            //                         select f.ConnectionId).ToList();

            //// Broad cast message to friend list
            //friendListConnectionId.Add(Context.ConnectionId);


            //string title = "";
            //string description = "";
            //string src = "";
            //string url = "";

            //var link = new Link(message) ;
            //if(!string.IsNullOrEmpty(link.Url))
            //{
            //    url = link.Url;
            //    using (var client = new WebClient())
            //    { 
            //        //client.DownloadData
            //        client.Encoding = Encoding.UTF8;
            //        string html = client.DownloadString(url);
            //        Document doc = NSoup.NSoupClient.Parse(html);

            //        // get title
            //        title = doc.Select("title").First.Text();               

            //        // get description
            //        if (doc.Select("meta[name=description]") != null)
            //        {
            //            if (doc.Select("meta[name=description]").First != null)
            //            { 
            //                description = doc.Select("meta[name=description]").First.Attr("content");
            //                if (string.IsNullOrEmpty(description))
            //                {
            //                    description = title;
            //                }
            //            }
            //        }
                
            //        // get image
            //        Elements imgs = doc.Select("img");
            //        List<Img> images = new List<Img>();
            //        foreach (var i in imgs)
            //        {
            //            images.Add(new Img(i.Attr("height"), i.Attr("width"), i.Attr("src")));
            //        }
            //        src = Img.GetSrcLargetestImage(images);

            //        if(string.IsNullOrEmpty(src))
            //        {
            //            var EleIcons = doc.Select("link[rel=icon]");
            //            if(EleIcons.Count > 0)
            //            {
            //                src = EleIcons.First.Attr("href");
            //            }
            //        }
            //    }
            //}

            //Clients.Clients(friendListConnectionId).messageReceived(userName, src, title, description);
        }


        public void SendPrivateMessage(string userRetrieved_Id, string message)
        {
            this.Init();

            var fromUser = this._chatRepository.GetUserByUserId(ConnectedUsers, userRetrieved_Id);

            // store message to database
            var msg = new PrivateMessage { UserSent_Id = this._curUser.UserId, UserRetrieved_Id = userRetrieved_Id, Content = message };
            this._privateMessageRepository.InsertPrivateMessage(msg);

            // friend online
            if (fromUser.ConnectionId != null)
            {
                Clients.Client(fromUser.ConnectionId).privateMessageReceived(this._curUser.UserId, message);
            }
        }

    }
}