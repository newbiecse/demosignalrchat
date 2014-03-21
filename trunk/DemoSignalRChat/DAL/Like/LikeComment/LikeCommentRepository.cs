using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class LikeCommentRepository : ILikeCommentRepositoty
    {
        private ApplicationDbContext _db;
        public LikeCommentRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }
        public IEnumerable<string> GetListUserIdLikedComment(string commentId)
        {
            return this._db.LikeComments
                .Where(l => l.CommentId == commentId)
                .Select(lc => lc.UserId);
        }

        public IEnumerable<UserViewModel> GetListUserLikedComment(string commentId)
        {
            var listUserIdLikedComment = this.GetListUserIdLikedComment(commentId);
            IUserRepository userRepository = new UserRepository(this._db);
            return userRepository.GetRangeUser(listUserIdLikedComment);
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