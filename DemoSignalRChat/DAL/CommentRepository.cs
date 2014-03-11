using DemoSignalRChat.Models;
using DemoSignalRChat.Preview;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class CommentRepository : ICommentRepository
    {
        private ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public CommentViewModel GetCommentByCommentId(string commentId)
        {
            var comment = this._db.Comments.ToList().Find(cmt => cmt.CommentId == commentId);
            IUserRepository userRepository = new UserRepository(this._db);
            var userOwner = userRepository.GetUserById(comment.UserId);

            ILikeCommentRepositoty likeCommentRepository = new LikeCommentRepository(this._db);
            var listUserLiked = likeCommentRepository.GetListUserLikedComment(commentId);

            return
                new CommentViewModel
                {
                    CommentId = comment.CommentId,
                    TimeCommented = comment.TimeComment,
                    Content = ProcessComment.ProcessMessage(comment.Content),
                    UserOwner = userOwner,
                    ListUserLiked = listUserLiked
                };
        }

        public IEnumerable<string> GetCommentIdForStatusId(string statusId)
        {
            return this._db.Comments
                .Where(c => c.StatusId == statusId)
                .Select(c => c.CommentId);
        }

        public IEnumerable<CommentViewModel> GetCommentForStatus(string statusId)
        {
            var commentIdList = this.GetCommentIdForStatusId(statusId);
            List<CommentViewModel> commentList = new List<CommentViewModel>();
            foreach(var commentId in commentIdList)
            {
                commentList.Add(this.GetCommentByCommentId(commentId));
            }
            return commentList;
        }


        public void AddComment(Comment comment)
        {
            this._db.Comments.Add(comment);
            this.Save();
        }

        private void Save()
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