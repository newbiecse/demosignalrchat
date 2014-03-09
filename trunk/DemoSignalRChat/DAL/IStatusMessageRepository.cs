using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusMessageRepository : IDisposable
    {
        string GetMessageByStatusId(string statusId);
        void Save();
    }
}
