using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface ICommentRepository : IDisposable
    {
        void AddComment(Comment comment);
        CommentViewModel GetCommentByCommentId(string commentId);
        IEnumerable<string> GetCommentIdForStatusId(string statusId);
        IEnumerable<CommentViewModel> GetCommentForStatus(string statusId);
    }
}
