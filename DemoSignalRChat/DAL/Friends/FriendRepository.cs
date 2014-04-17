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

        private IEnumerable<string> GetListFriendMutual(IEnumerable<string> listFriendUserId1, IEnumerable<string> listFriendUserId2)
        {
            return from userId1 in listFriendUserId1
                   join userId2 in listFriendUserId2
                   on userId1 equals userId2
                   select userId1;
        }

        private IEnumerable<string> GetListFriendMutualByUserID(IEnumerable<string> listFriendUserId1, string userId2)
        {
            var listFriendUserId2 = GetFriendListId(userId2);
            return GetListFriendMutual(listFriendUserId1, listFriendUserId2);
        }

        

        private IEnumerable<string> GetFriendIdsRandom(IEnumerable<string> friendListId)
        {
            Random rnd = new Random();

            int r = rnd.Next(0, friendListId.Count());
            return friendListId.OrderBy(x => rnd.Next(r))
                .Take(2)
                .Distinct();
        }


        public IEnumerable<string> GetFriendIdsNotIsFriened(IEnumerable<string> friendListIds, string userId, string friendId)
        {
            List<string> userIdExcept = new List<string>();
            var friendIdsOFFriend = GetFriendListId(friendId).ToList();

            userIdExcept.AddRange(friendListIds);
            userIdExcept.Add(userId);

            var FriendIdsNotIsFriened = friendIdsOFFriend.Except(userIdExcept);

            return FriendIdsNotIsFriened;
        }

        private FriendSugestViewModel GetFriendSugest(IEnumerable<string> friendListId, string friendIdSugest)
        {
            IUserRepository userRepository = new UserRepository(this._db);

            var friendSugest = userRepository.GetUserById(friendIdSugest);

            var friendIdsMutual = GetListFriendMutualByUserID(friendListId, friendIdSugest);
            var listFriendMutual = userRepository.GetRangeUser(friendIdsMutual);
            return new FriendSugestViewModel
            {
                UserId = friendSugest.UserId,
                Displayname = friendSugest.Displayname,
                Avatar = friendSugest.Avatar,
                ListFriendMutual = listFriendMutual
            };
        }

        public IEnumerable<FriendSugestViewModel> GetFriendListSugest(string userId)
        {
            var friendListId = GetFriendListId(userId);
            var friendIdsRandom = GetFriendIdsRandom(friendListId);

            //List<FriendSugest> friendListIdSugest = new List<FriendSugest>();

            List<string> friendListIdsPredict = new List<string>();

            foreach (var friendId in friendIdsRandom)
            {
                var friendIdsNotIsFriened = GetFriendIdsNotIsFriened(friendListId, userId, friendId);

                if (friendIdsNotIsFriened != null && friendIdsNotIsFriened.Count() > 0)
                {
                    friendListIdsPredict.AddRange(friendIdsNotIsFriened);
                }                
            }

            var friendIdsSugest = GetFriendIdsRandom(friendListIdsPredict);

            List<FriendSugestViewModel> friendsSugest = new List<FriendSugestViewModel>();
            foreach(var friendIdSugest in friendIdsSugest)
            {
                friendsSugest.Add(GetFriendSugest(friendListId, friendIdSugest));
            }

            return friendsSugest;
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

        public IEnumerable<string> GetFriendListIdWaitAccept(string userId)
        {
            var friendListId_1 = (from f in this._friends
                                  where f.UserId == userId && f.FriendStatus == FriendRepository.WAITACCEPT
                                  select f.FriendId).ToList();

            var friendListId_2 = (from f in this._friends
                                  where f.FriendId == userId && f.FriendStatus == FriendRepository.WAITACCEPT
                                  select f.UserId).ToList();

            return friendListId_1.Union(friendListId_2).ToList();
        }

        public IEnumerable<UserViewModel> GetFriendListWaitAccept(string userId)
        {
            var userRepository = new UserRepository(this._db);
            return userRepository.GetRangeUser(this.GetFriendListIdWaitAccept(userId));
        }

        public IEnumerable<UserViewModel> GetFriendList(string userId)
        {
            var userRepository = new UserRepository(this._db);
            return userRepository.GetRangeUser(this.GetFriendListId(userId));
        }

        public void AddFriend(string userId, string friendId)
        {
            this._db.Friends.Add(new Friend { UserId = userId, FriendId = friendId, FriendStatus = FriendRepository.WAITACCEPT });
            this.Save();
        }

        public void AcceptFriend(string userId, string friendId)
        {
            var accept = this._db.Friends.First(f => f.UserId == userId && f.FriendId == friendId);
            accept.FriendStatus = FriendRepository.ISFRIENDED;
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