using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IUserRepository : IDisposable
    {
        UserViewModel GetUserById(string userId);
        IEnumerable<UserViewModel> GetRangeUser(IEnumerable<string> listUserId);
    }
}
