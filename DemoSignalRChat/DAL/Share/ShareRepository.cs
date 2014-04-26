using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class ShareRepository : IShareRepository
    {
        private ApplicationDbContext _db;

        public ShareRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public IEnumerable<string> GetStatusIdShared(string userId)
        {
            return (from share in this._db.Shares
                   where share.UserId == userId
                   select share.StatusId)
                   .OrderByDescending(s => s)
                   .Distinct();
        }

        public void AddShare(Share share)
        {
            this._db.Shares.Add(share);
            this.Save();
        }

        public int GetNumShare(string statusId)
        {
            return this._db.Shares.Count(sh => sh.StatusId == statusId);
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