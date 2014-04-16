using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class WallRepository : IWallRepository
    {
        private ApplicationDbContext _db;
        private IStatusRepository statusRepository;
        private IShareRepository shareRepository;

        public WallRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
            statusRepository = new StatusRepository(dbContext);
            shareRepository = new ShareRepository(dbContext);
        }

        //public 

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