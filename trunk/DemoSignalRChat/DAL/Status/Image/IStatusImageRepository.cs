using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusImageRepository : IDisposable
    {
        IEnumerable<string> GetListImage(string statusId);
        void AddImage(string statusId, string image);
        void AddRangeImage(string statusId, string[] image);
    }
}
