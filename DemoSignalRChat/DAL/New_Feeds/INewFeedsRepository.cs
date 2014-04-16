using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSignalRChat.DAL.New_Feeds
{
    interface INewFeedsRepository
    {
        void AddNewFeed(NewFeeds newFeed);
        IEnumerable<NewFeedsViewModel> GetListNewFeeds(string userId);
    }
}
