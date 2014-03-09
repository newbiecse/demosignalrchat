using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class StatusMessageRepository : IStatusMessageRepository
    {
        private ApplicationDbContext _db;

        public StatusMessageRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public string GetMessageByStatusId(string statusId)
        {
            var sttMessage = this._db.StatusMessages.Find(statusId);
            if(sttMessage == null)
            {
                return null;
            }
            return sttMessage.Message;
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