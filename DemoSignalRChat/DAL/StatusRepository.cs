using DemoSignalRChat.Models;
using DemoSignalRChat.Preview;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class StatusRepository : IStatusRepository
    {
        private ApplicationDbContext _db;
        public StatusRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public IEnumerable<string> GetListStatusId(string userId)
        {
            return this._db.Status
                .OrderByDescending(stt => stt.StatusId)
                .Take(10)
                .Select(stt => stt.StatusId);
        }

        public IEnumerable<string> GetListStatusIdNewest(string userId)
        {
            IFriendRepository friendRepository = new FriendRepository(this._db);

            List<string> listUserIdBoardcast = new List<string>();

            var friendListUserId = friendRepository.GetFriendListId(userId);

            if(friendListUserId != null && friendListUserId.Count() > 0)
            {
                listUserIdBoardcast.AddRange(friendListUserId);
            }

            listUserIdBoardcast.Add(userId);

            List<string> listStatusIdNewest = new List<string>();
            foreach (var id in listUserIdBoardcast)
            {
                var listStatusId = this.GetListStatusId(id);
                listStatusIdNewest.AddRange(listStatusId);
            }

            return listStatusIdNewest.OrderByDescending(p => p)
                .Take(5);
        }

        public Status GetStatusByStatusId(string statusId)
        {
            return this._db.Status.Find(statusId);
        }

        public StatusViewModel GetStatusViewModelByStatusId(string statusId)
        {
            Status status = this.GetStatusByStatusId(statusId);
            IStatusLocationRepository sttLocation = new StatusLocationRepository(this._db);
            IStatusMessageRepository sttMessageRepository = new StatusMessageRepository(this._db);
            IStatusImageRepository sttImageRepository = new StatusImageRepository(this._db);
            IUserRepository userRepository = new UserRepository(this._db);
            ILikeRepository likeRepository = new LikeRepository(this._db);
            IShareRepository shareRepository = new ShareRepository(this._db);
            ICommentRepository commentRepository = new CommentRepository(this._db);

            var message = sttMessageRepository.GetMessageByStatusId(statusId);
            var messageProcessed = ProcessMessage.ProcMessage(message);

            LinkPreview linkPreview = ProcessMessage.GetFirstLinkPreview(message);

            var userOwner = userRepository.GetUserById(status.UserId);

            var statusViewModel = new
                StatusViewModel
                {
                    StatusId = statusId,
                    TimePost = status.TimePost,
                    Location = sttLocation.GetLocationForStatus(statusId),
                    Message = messageProcessed,
                    Images = sttImageRepository.GetListImage(statusId),
                    UserOwner = userOwner,
                    LinkPreview = linkPreview,
                    NumShared = shareRepository.GetNumShare(statusId),
                    ListUserLiked = likeRepository.GetListUserLiked(statusId),
                    ListCommented = commentRepository.GetCommentForStatus(statusId)
                };
            return statusViewModel;
        }

        public IEnumerable<StatusViewModel>  GetListStatusNewest(string userId)
        {
            var listStatusIdNewest = this.GetListStatusIdNewest(userId).Distinct();

            List<StatusViewModel> listStatusNewest = new List<StatusViewModel>();

            foreach(string statusId in listStatusIdNewest)
            {
               var statusNewest = this.GetStatusViewModelByStatusId(statusId);
                listStatusNewest.Add(statusNewest);
            }

            return listStatusNewest;
        }

        public List<Status> GetMoreListStatus(string userId, DateTime TimePost)
        {
            return this._db.Status.Where(stt => stt.UserId == userId && stt.TimePost > TimePost)
                .OrderByDescending(stt => stt.TimePost)
                .Take(3).ToList();
        }



        //--------------------------------------------//--------------------------------------------
        public void AddStatus(Status status)
        {
            this._db.Status.Add(status);
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