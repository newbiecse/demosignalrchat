﻿using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class ChatRepository : IChatRepository
    {
        private ApplicationDbContext _db;
        public ChatRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public UserChatViewModel GetUserByConnectionId(List<UserChatViewModel> connectedUsers, string connectionId)
        {
            var userId = connectedUsers.Find(u => u.ConnectionId == connectionId).UserId;
            var user =  _db.Users.Find(userId);
            return new UserChatViewModel { ConnectionId = connectionId, UserId = userId, Displayname = user.DisplayName, Avatar = user.Avatar };
        }

        public UserChatViewModel GetUserByUserId(List<UserChatViewModel> connectedUsers, string userId)
        {
            return connectedUsers.Find(u => u.UserId == userId);
        }

        public bool IsConnected(List<UserChatViewModel> connectedUsers, string connectionId)
        {
            if(connectedUsers.Count(u => u.ConnectionId == connectionId) > 0)
            {
                return true;
            }
            return false;
        }

        public List<UserChatViewModel> GetFriendListOnline(List<UserChatViewModel> connectedUsers, List<string> friendListId, string userId)
        {
            return (from u in connectedUsers
                    where friendListId.Contains(u.UserId)
                    select u)
                    .ToList();
        }

        public List<string> GetFriendListId_Online(List<UserChatViewModel> friendListOnline)
        {
            return (from f in friendListOnline
                    select f.UserId).ToList();
        }

        public List<string> GetFriendList_ConnectionId(List<UserChatViewModel> friendListOnline)
        {
            return (from f in friendListOnline
                    select f.ConnectionId).ToList();
        }

        public List<string> GetAllUserRelate_ConnectionId(List<string> friendList_ConnectionId, string meConnectionId)
        {
            var meAndFriend_ConnectionId = friendList_ConnectionId;
            meAndFriend_ConnectionId.Add(meConnectionId);
            return meAndFriend_ConnectionId;
        }

        public void RemoveUserConnected(List<UserChatViewModel> connectedUsers, UserChatViewModel user)
        {
            connectedUsers.Remove(user);
        }

        private bool disposed = false;
 
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._db.Dispose();
                }
            }
            this.disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}