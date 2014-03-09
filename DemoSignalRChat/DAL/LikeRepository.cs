using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class LikeRepository : ILikeRepository
    {
        private ApplicationDbContext _db;

        public LikeRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
            
        }

        public IEnumerable<string> GetListUserIdLiked(string statusId)
        {
            return this._db.Likes
                .Where(l => l.StatusId == statusId)
                .Select(l => l.UserId);
        }

        public IEnumerable<UserViewModel> GetListUserLiked(string statusId)
        {
            var listUserIdLiked = this.GetListUserIdLiked(statusId);
            IUserRepository userRepository = new UserRepository(this._db);
            return userRepository.GetRangeUser(listUserIdLiked);
        }

        public bool IsLiked(string statusId, string userId)
        {
            if(this._db.Likes.Find(statusId, userId) != null)
            {
                return true;
            }
            return false;
        }

        public void FuncLike(string statusId, string userId)
        {
            this._db.Likes.Add(new Like { UserId = userId, StatusId = statusId });
            this.Save();
        }


        public void FuncUnLike(string statusId, string userId)
        {
            var unLike = this._db.Likes.Find(statusId, userId);
            this._db.Likes.Remove(unLike);
            this.Save();
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