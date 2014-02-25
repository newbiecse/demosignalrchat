using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IPrivateMessageRepository : IDisposable
    {
        IEnumerable<PrivateMessage> GetPrivateMessages(string userSentId, string userRetrieveId);
        void InsertPrivateMessage(PrivateMessage privateMessage);
        void Save();
    }
}
