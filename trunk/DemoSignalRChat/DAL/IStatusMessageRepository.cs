using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusMessageRepository : IDisposable
    {
        IEnumerable<StatusMessage> GetStatusMessages();
        StatusMessage GetStatusMessageByID(string statusId);
        void InsertStatusMessage(StatusMessage statusMessage);
        void DeleteStatusMessage(int statusId);
        void UpdateStatusMessage(StatusMessage statusMessage);
        void Save();
    }
}
