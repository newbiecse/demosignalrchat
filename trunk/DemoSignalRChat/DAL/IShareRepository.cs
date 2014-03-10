using DemoSignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IShareRepository : IDisposable
    {
        void AddShare(Share share);
        int GetNumShare(string statusId);
    }
}
