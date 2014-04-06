using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IFriendRepository : IDisposable
    {
        IEnumerable<string> GetFriendListId(string userId);
        IEnumerable<UserViewModel> GetFriendList(string userId);

        IEnumerable<FriendSugestViewModel> GetFriendListSugest(string userId);

        void AddFriend(string userId, string friendId);
        void AcceptFriend(string userId, string friendId);
        IEnumerable<UserViewModel> GetFriendListWaitAccept(string userId);
        void Save();
    }
}
