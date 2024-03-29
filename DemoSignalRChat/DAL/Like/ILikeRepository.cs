﻿using DemoSignalRChat.Models;
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
        int IsLiked(string statusId, string userId);
        int Count(string statusId);
        void Like(Like like);
        void UnLike(string statusId, string userId);
        void Save();
    }
}
