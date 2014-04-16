using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public UserViewModel GetUserById(string userId)
        {
            var appUser = this._db.Users.Find(userId);
            return new UserViewModel { UserId = appUser.Id, UserName = appUser.DisplayName, Avatar = appUser.Avatar };                
        }

        public IEnumerable<UserViewModel> GetRangeUser(IEnumerable<string> listUserId)
        {
            List<UserViewModel> rangeUsers = new List<UserViewModel>();
            foreach(var userId in listUserId)
            {
                rangeUsers.Add(this.GetUserById(userId));
            }
            return rangeUsers;
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