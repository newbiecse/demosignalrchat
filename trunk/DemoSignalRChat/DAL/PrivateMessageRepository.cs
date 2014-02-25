using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        private ApplicationDbContext _db;
        public PrivateMessageRepository(ApplicationDbContext dbContext)
        {
            this._db = dbContext;
        }
        public IEnumerable<PrivateMessage> GetPrivateMessages(string userSentId, string userRetrieveId)
        {
            var m = (from msg in this._db.PrivateMessages
                    where msg.UserSent_Id == userSentId && msg.UserRetrieved_Id == userRetrieveId
                    select msg).ToList();
            return m;                
        }

        public void InsertPrivateMessage(PrivateMessage privateMessage)
        {
            this._db.PrivateMessages.Add(privateMessage);
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