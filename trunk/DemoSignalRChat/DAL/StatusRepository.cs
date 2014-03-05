using DemoSignalRChat.Models;
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
        public IEnumerable<StatusMessage> GetStatusMessages()
        {
            return this._db.StatusMessages.ToList();
        }

        public StatusMessage GetStatusMessageByID(string statusId)
        {
            return this._db.StatusMessages.Find(statusId);
        }

        //--------------------------------------------//--------------------------------------------
        public void AddStatus(Status status)
        {
            this._db.Status.Add(status);
            this.Save();
        }
        public void AddStatusLocation(StatusLocation statusLocation)
        {
            this._db.StatusLocations.Add(statusLocation);
            this.Save();
        }
        public void AddStatusImage(StatusImage statusImage)
        {
            this._db.StatusImages.Add(statusImage);
            this.Save();
        }
        public void AddStatusMessage(StatusMessage statusMessage)
        {
            this._db.StatusMessages.Add(statusMessage);
            this.Save();
        }
        //--------------------------------------------//--------------------------------------------
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