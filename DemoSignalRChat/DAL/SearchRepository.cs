using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class SearchRepository : ISearchRepository
    {
        private ApplicationDbContext _db;

        public SearchRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public IEnumerable<UserViewModel> Search(string searchParam)
        {
            return from u in this._db.Users
                   where u.UserName.ToLower().Contains(searchParam.ToLower())
                   select new UserViewModel { UserId = u.Id, UserName = u.UserName, Avatar = u.Avatar };
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