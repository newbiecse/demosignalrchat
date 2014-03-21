using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface ILikeCommentRepositoty : IDisposable
    {
        IEnumerable<string> GetListUserIdLikedComment(string commentId);
        IEnumerable<UserViewModel> GetListUserLikedComment(string commentId);
    }
}
