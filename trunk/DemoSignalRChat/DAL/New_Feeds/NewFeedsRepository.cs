using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL.New_Feeds
{
    public class NewFeedsRepository : INewFeedsRepository
    {
        private ApplicationDbContext _db;
        const int POST_STATUS = 0;
        const int COMMENT = 1;
        const int LIKE = 2;
        const int SHARE = 3;
        const int ADD_FRIEND = 4;

        public NewFeedsRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public void AddNewFeed(NewFeeds newFeed)
        {
            this._db.NewFeeds.Add(newFeed);
            this.Save();
        }

        private IEnumerable<NewFeeds> GetNewFeeds(string userId)
        {
            IFriendRepository friendRepository = new FriendRepository(this._db);
            var friendListId = friendRepository.GetFriendListId(userId);
            return this._db.NewFeeds.Where(n => friendListId.Contains(n.UserId))
                .OrderByDescending(n => n.NewFeedId)
                .Take(5);
        }

        //private string GetContentNew(int typeActionId)
        //{
        //    switch(typeActionId)
        //    {
        //        case
        //    }
        //}

        public IEnumerable<NewFeedsViewModel> GetListNewFeeds(string userId)
        {
            var newFeeds = GetNewFeeds(userId);

            IUserRepository userRepository = new UserRepository(this._db);
            IStatusRepository statusRepository = new StatusRepository(this._db);
            ICommentRepository commentRepository = new CommentRepository(this._db);

            List<NewFeedsViewModel> newFeedsViewModel = new List<NewFeedsViewModel>();
            foreach(var nf in newFeeds)
            {
                var newFeed = new NewFeedsViewModel ();

                newFeed.TypeAtionId = nf.TypeActionId;
                newFeed.Friend = userRepository.GetUserById(nf.UserId);

                switch(nf.TypeActionId)
                {
                    case POST_STATUS:
                    case SHARE:
                    case LIKE:
                        newFeed.Status = statusRepository.GetShortStatusByStatusId(nf.StatusId_Or_UserId);
                        break;
                    case COMMENT:
                        newFeed.Comment = commentRepository.GetCommentByCommentId(nf.StatusId_Or_UserId);
                        break;
                    case ADD_FRIEND:
                        newFeed.AnotherUser = userRepository.GetUserById(nf.StatusId_Or_UserId);
                        break;
                }

                newFeedsViewModel.Add(newFeed);
            }
            return newFeedsViewModel;
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