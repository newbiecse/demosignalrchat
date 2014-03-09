using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface ILikeRepository : IDisposable
    {
        IEnumerable<string> GetListUserIdLiked(string statusId);
        IEnumerable<UserViewModel> GetListUserLiked(string statusId);
        bool IsLiked(string statusId, string userId);
        void FuncLike(string statusId, string userId);
        void FuncUnLike(string statusId, string userId);
        void Save();
    }
}
