using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusLocationRepository : IDisposable
    {
        void AddLocation(string statusId, string location);
        string GetLocationForStatus(string statusId);
        void Save();
    }
}
