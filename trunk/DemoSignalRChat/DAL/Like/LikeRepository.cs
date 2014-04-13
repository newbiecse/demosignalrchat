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

        public int IsLiked(string statusId, string userId)
        {
            if(this._db.Likes.Count(l => l.UserId == userId && l.StatusId == statusId) > 0)
            {
                return 1;
            }
            return 0;
        }

        public int Count(string statusId)
        {
            return this._db.Likes.Count(l => l.StatusId == statusId);
        }


        public void Like(Like like)
        {
            this._db.Likes.Add(like);
            this.Save();
        }


        public void UnLike(string statusId, string userId)
        {
            var liked = this._db.Likes.First(l => l.UserId == userId && l.StatusId == statusId);
            this._db.Likes.Remove(liked);
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