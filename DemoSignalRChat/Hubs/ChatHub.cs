using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DemoSignalRChat.Models;
using DemoSignalRChat.DAL;
using Microsoft.Ajax.Utilities;
using DemoSignalRChat.ViewModels;
using System.Threading.Tasks;

namespace DemoSignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        #region members

        static List<User> ConnectedUsers = new List<User>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        ApplicationDbContext db = new ApplicationDbContext();
        User _curUser;
        List<string> _friendIdList;
        List<UserChatViewModel> _friendListOnline = new List<UserChatViewModel>();
        //List<string> _connectedIdList;

        #endregion

        public void SetCurUser()
        {
            // find current user in ConnectedUsers
            this._curUser = ConnectedUsers.Find(u => u.ConnectionID == Context.ConnectionId);
            this._curUser.UserName = this.db.Users.Single(u => u.Id == this._curUser.Id).UserName;
        }

        public User GetUserById(string userId)
        {
            //var App = (User)this.db.Users.ToList().Find(u => u.Id == userId);
            var appUser = this.db.Users.Single(u => u.Id == userId);

            var user = new User { Id = appUser.Id, UserName = appUser.UserName };

            var uConnected = ConnectedUsers.Find(u => u.Id == user.Id);

            if (uConnected != null)
            {
                user.ConnectionID = uConnected.ConnectionID;
            }
            return user;
        }
        
        public void SetFriendIdList()
        {
            // get friend list of current user
            var listFriend_1 = from f in this.db.Friends
                               where f.FriendId == this._curUser.Id
                               select f.UserId;

            var listFriend_2 = from f in this.db.Friends
                               where f.UserId == this._curUser.Id
                               select f.FriendId;

            this._friendIdList = listFriend_1.Union(listFriend_2).Distinct().ToList();
        }
       
        public void SetFriendListOnline()
        {
            this._friendListOnline = (from u in ConnectedUsers
                                      where this._friendIdList.Contains(u.Id)
                                      select new UserChatViewModel { UserId = u.Id, ConnectionId = u.ConnectionID }).ToList();
        }

        public void init()
        {
            this.SetCurUser();
            this.SetFriendIdList();
            //this.SetConnectedIdList();
            this.SetFriendListOnline();
        }

        public void Connect(string userID)
        {
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionID == id) == 0)
            {
                ConnectedUsers.Add(new User { ConnectionID = id, Id = userID });

                this.init();

                var tFriendIdListOnline = (from f in this._friendListOnline
                                           select f.UserId).ToList();

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(userID);

                // send to caller
                Clients.Caller.onConnected(tFriendIdListOnline);
            }
        }

        public override Task OnDisconnected()
        {
            var id = Context.ConnectionId;

            this.init();

            var tFriendIdListOnline = (from f in this._friendListOnline
                                       select f.UserId).ToList();

            // send to all except caller client
            var tConnectionIdListOnline = (from f in this._friendListOnline
                                           select f.ConnectionId).ToList();

            ConnectedUsers.Remove(ConnectedUsers.Where(u => u.ConnectionID == id).Single());

            Clients.Clients(tConnectionIdListOnline).signOut(this._curUser.Id);
            
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.
            return base.OnDisconnected();
        }

        public void TurnOffChat()
        {
                var id = Context.ConnectionId;

                this.init();

                var tFriendIdListOnline = (from f in this._friendListOnline
                                           select f.UserId).ToList();

                // send to caller
                Clients.Caller.onConnected(tFriendIdListOnline);

                // Broad cast message to friend list
                var fListConnectionId = (from f in this._friendListOnline
                                           select f.ConnectionId).ToList();

                Clients.Clients(fListConnectionId).offLine(this._curUser.Id);
        }

        public void SendMessageToAll(string userName, string message)
        {
            this.init();

            // store message to database
            this.db.StatusMessages.Add(new StatusMessage { UserId = this._curUser.Id, Message = message });
            this.db.SaveChanges();

            var fListConnectionId = (from f in this._friendListOnline
                                     select f.ConnectionId).ToList();

            // Broad cast message to friend list
            Clients.Clients(fListConnectionId).messageReceived(userName, message);
        }

        public void SendPrivateMessage(string userRetrieved_Id, string message)
        {
            this.init();

            var fromUser = this.GetUserById(userRetrieved_Id);

            // store message to database
            this.db.PrivateMessages.Add(new PrivateMessage { UserSent_Id = this._curUser.Id, UserRetrieved_Id = userRetrieved_Id, Content = message });
            db.SaveChanges();

            /*
             * Broad cast message to friend
             */
            // friend is online
            if (fromUser.ConnectionID != null)
            {
                Clients.Client(fromUser.ConnectionID).privateMessageReceived(this._curUser.Id, message);
            }
        }

    }
}