﻿using System.Collections.Generic;
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
using DemoSignalRChat.ProcessPreData;


namespace DemoSignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        #region members

        ApplicationDbContext _dbContext;

        IFriendRepository _friendRepository;
        IChatRepository _chatRepository;
        IPrivateMessageRepository _privateMessageRepository;
        IStatusRepository _statusRepository;
        IStatusMessageRepository _statusMessageRepository;
        IStatusLocationRepository _statusLocationRepository;
        ILikeRepository _likeRepository;
        IShareRepository _shareRepository;

        List<string> _friendListId;
        List<UserChatViewModel> _friendListOnline;
        List<string> _friendListConnectionId_Online;
        List<string> _allUserRelate_ConnectionId;

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
            this._statusRepository = new StatusRepository(this._dbContext);
            this._statusMessageRepository = new StatusMessageRepository(this._dbContext);
            this._statusLocationRepository = new StatusLocationRepository(this._dbContext);
            this._likeRepository = new LikeRepository(this._dbContext);
            this._shareRepository = new ShareRepository(this._dbContext);

            // get current connectionId
            this._curConnectionId = this.Context.ConnectionId;

            // get chatViewModel of User via connectionId
            this._curUser = this._chatRepository.GetUserByConnectionId(ConnectedUsers, this.Context.ConnectionId);

            // get friendListId
            this._friendListId = this._friendRepository.GetFriendListId(this._curUser.UserId).ToList();

            // get friendListOnline
            this._friendListOnline = this._chatRepository.GetFriendListOnline(ConnectedUsers, this._friendListId, this._curUser.UserId);

            // get friendListConnectionId
            this._friendListConnectionId_Online = this._chatRepository.GetFriendList_ConnectionId(this._friendListOnline);

            this._allUserRelate_ConnectionId = this._chatRepository.GetAllUserRelate_ConnectionId(this._friendListConnectionId_Online, this._curUser.ConnectionId);
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
                var friendListId_OnLine = this._chatRepository.GetFriendListId_Online(this._friendListOnline);
                Clients.Caller.onConnected(friendListId_OnLine);
            }
        }

        public override Task OnDisconnected()
        {
            this.Init();

            // get friend list id online
            var friendListId_OnLine = this._chatRepository.GetFriendListId_Online(this._friendListOnline);

            // send to all friend list online
            var friendList_connectionID = this._chatRepository.GetFriendList_ConnectionId(this._friendListOnline);

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

                var tFriendIdListOnline = (from f in this._friendListOnline
                                           select f.UserId).ToList();

                // send to caller
                Clients.Caller.onConnected(tFriendIdListOnline);

                // Broad cast message to friend list
                var fListConnectionId = (from f in this._friendListOnline
                                           select f.ConnectionId).ToList();

                Clients.Clients(fListConnectionId).offChat(this._curUser.UserId);
        }


        public void Like(string statusId)
        {
            this.Init();
            var like = new Like { TimeLiked = DateTime.Now, StatusId = statusId, UserId = this._curUser.UserId };
            this._likeRepository.Like(like);

            Clients.Clients(this._allUserRelate_ConnectionId).like(this._curUser.UserName, statusId);
        }

        public void SendMessageToAll(string message, string location)
        {
            this.Init();

            var statusId = SequentialGuid.Create();

            Status status = new Status{StatusId = statusId, UserId = this._curUser.UserId};
            this._statusRepository.AddStatus(status);

            this._statusMessageRepository.AddMessage(new StatusMessage { StatusId = statusId, Message = message });
            
            if(!string.IsNullOrEmpty(location))
            {
                this._statusLocationRepository.AddLocation(statusId, location);
            }


            var userViewModel = new UserViewModel { UserId = this._curUser.UserId, UserName = this._curUser.UserId, Avatar = this._curUser.Avatar };

            message = ProcessMessage.ProcessMessageStatus(userViewModel, message);

            Clients.Clients(this._allUserRelate_ConnectionId).messageReceived(this._curUser.UserName, message);
        }


        public void SendPrivateMessage(string userRetrieved_Id, string message)
        {
            this.Init();

            var fromUser = this._chatRepository.GetUserByUserId(ConnectedUsers, userRetrieved_Id);

            // store message to database
            var msg = new PrivateMessage { UserSent_Id = this._curUser.UserId, UserRetrieved_Id = userRetrieved_Id, Content = message };
            this._privateMessageRepository.InsertPrivateMessage(msg);

            // friend online
            if (fromUser != null)
            {
                Clients.Client(fromUser.ConnectionId).privateMessageReceived(this._curUser.UserId, message);
            }
        }

    }
}