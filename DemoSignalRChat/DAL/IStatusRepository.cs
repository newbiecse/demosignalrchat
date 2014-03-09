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
        IEnumerable<string> GetListStatusId(string userId);
        IEnumerable<string> GetListStatusIdNewest(string userId);
        Status GetStatusByStatusId(string statusId);
        StatusViewModel GetStatusViewModelByStatusId(string statusId);
        IEnumerable<StatusViewModel> GetListStatusNewest(string userId);
        List<Status> GetMoreListStatus(string userId, DateTime TimePost);

        void AddStatus(Status status);
        void AddStatusLocation(StatusLocation statusLocation);
        void AddStatusImage(StatusImage statusImage);
        void AddStatusMessage(StatusMessage statusMessage);


        void DeleteStatusMessage(int statusId);
        void UpdateStatusMessage(StatusMessage statusMessage);
        void Save();
    }
}
