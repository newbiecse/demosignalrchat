using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL
{
    interface IStatusRepository : IDisposable
    {
        IEnumerable<StatusViewModel> GetListStatusNewest(string userId);
        StatusViewModel GetShortStatusByStatusId(string statusId);
        List<Status> GetMoreListStatus(string userId, DateTime TimePost);
        void AddStatus(Status status);
    }
}
