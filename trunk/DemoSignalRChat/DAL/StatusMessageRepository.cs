using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public IEnumerable<StatusMessage> GetStatusMessages()
        {
            return this._db.StatusMessages.ToList();
        }

        public StatusMessage GetStatusMessageByID(string statusId)
        {
            return this._db.StatusMessages.Find(statusId);
        }

        public void InsertStatusMessage(StatusMessage statusMessage)
        {
            this._db.StatusMessages.Add(statusMessage);
        }

        public void DeleteStatusMessage(int statusId)
        {
            StatusMessage statusMessage = this._db.StatusMessages.Find(statusId);
            this._db.StatusMessages.Remove(statusMessage);
        }

        public void UpdateStatusMessage(StatusMessage statusMessage)
        {
            this._db.Entry(statusMessage).State = EntityState.Modified;
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