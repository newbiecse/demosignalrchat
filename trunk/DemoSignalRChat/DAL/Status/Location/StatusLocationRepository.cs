using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class StatusLocationRepository : IStatusLocationRepository
    {
        private ApplicationDbContext _db;
        public StatusLocationRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public string GetLocationForStatus(string statusId)
        {
            var sttLocation = this._db.StatusLocations.Find(statusId);
            if(sttLocation == null)
            {
                return null;
            }
            return sttLocation.Location;
        }

        public void AddLocation(string statusId, string location)
        {
            this._db.StatusLocations.Add(new StatusLocation { StatusId = statusId, Location = location });
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