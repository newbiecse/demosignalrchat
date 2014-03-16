using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface ISearchRepository : IDisposable
    {
        IEnumerable<UserViewModel> Search(string searchParam);
    }
}
