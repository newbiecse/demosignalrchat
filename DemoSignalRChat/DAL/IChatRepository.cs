using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IChatRepository : IDisposable
    {
        UserChatViewModel GetUserByConnectionId(List<UserChatViewModel> connectedUsers, string connectionId);
        UserChatViewModel GetUserByUserId(List<UserChatViewModel> connectedUsers, string userId);

        bool IsConnected(List<UserChatViewModel> connectedUsers, string connectionId);

        List<UserChatViewModel> GetFriendListOnline(List<UserChatViewModel> connectedUsers, List<string> friendListId, string userId);
    }
}
