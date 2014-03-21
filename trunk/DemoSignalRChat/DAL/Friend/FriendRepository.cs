using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class FriendRepository : IFriendRepository
    {
        private ApplicationDbContext _db;
        private IEnumerable<Friend> _friends;
        const int ISFRIENDED = 1;
        const int WAITACCEPT = 0;

        public FriendRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
            this._friends = dbContext.Friends;
        }

        public IEnumerable<string> GetFriendListId(string userId)
        {
            var friendListId_1 = (from f in this._friends
                                where f.UserId == userId && f.FriendStatus == FriendRepository.ISFRIENDED
                                select f.FriendId).ToList();

            var friendListId_2 = (from f in this._friends
                                  where f.FriendId == userId && f.FriendStatus == FriendRepository.ISFRIENDED
                                  select f.UserId).ToList();

            return friendListId_1.Union(friendListId_2).ToList();
        }

        public IEnumerable<UserViewModel> GetFriendList(string userId)
        {
            var userRepository = new UserRepository(this._db);
            return userRepository.GetRangeUser(this.GetFriendListId(userId));
        }

        public void AddFriend(string userId, string friendId)
        {
            this._db.Friends.Add(new Friend { UserId = userId, FriendId = friendId, FriendStatus = FriendRepository.WAITACCEPT });
        }

        public void Save()
        {
            this._db.SaveChanges();
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