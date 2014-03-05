using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusRepository : IDisposable
    {
        IEnumerable<StatusMessage> GetStatusMessages();
        StatusMessage GetStatusMessageByID(string statusId);


        void AddStatus(Status status);
        void AddStatusLocation(StatusLocation statusLocation);
        void AddStatusImage(StatusImage statusImage);
        void AddStatusMessage(StatusMessage statusMessage);


        void DeleteStatusMessage(int statusId);
        void UpdateStatusMessage(StatusMessage statusMessage);
        void Save();
    }
}
