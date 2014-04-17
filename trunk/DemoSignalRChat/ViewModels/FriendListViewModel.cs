using DemoSignalRChat.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public class FriendListViewModel
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public List<UserViewModel> FriendList { get; set; }
        public FriendListViewModel(string userId)
        {
            // get friend list of current user
            var listFriend_1 = from f in this.db.Friends
                               where f.FriendId == userId
                               select f.UserId;

            var listFriend_2 = from f in this.db.Friends
                               where f.UserId == userId
                               select f.FriendId;

            var friendIdList = listFriend_1.Union(listFriend_2).Distinct().ToList();

            this.FriendList = (from u in this.db.Users
                              where friendIdList.Contains(u.Id)
                              select new UserViewModel { UserId = u.Id, Displayname = u.UserName }).ToList();
        }
    }
}