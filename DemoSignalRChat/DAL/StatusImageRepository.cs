using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class StatusImageRepository : IStatusImageRepository
    {
        private ApplicationDbContext _db;
        public StatusImageRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }

        public void AddImage(string statusId, string image)
        {
            this._db.StatusImages.Add(new StatusImage { StatusId = statusId, Image = image });
            this.Save();
        }

        public void AddRangeImage(string statusId, string[] images)
        {
            foreach(var img in images)
            {
                this.AddImage(statusId, img);
            }
            this.Save();
        }


        public IEnumerable<string> GetListImage(string statusId)
        {
            return this._db.StatusImages
                .Where(sttImg => sttImg.StatusId == statusId)
                .Select(sttImg => sttImg.Image);
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