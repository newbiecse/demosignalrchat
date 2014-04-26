using System.Web.Mvc;
using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using DemoSignalRChat.DAL;
using Microsoft.AspNet.Identity;
using System.Net;
using NSoup.Nodes;
using System.Collections.Generic;
using NSoup.Select;
using DemoSignalRChat.Preview;
using System;
using System.Linq;
using System.IO;
using DemoSignalRChat.DAL.New_Feeds;

namespace DemoSignalRChat.Controllers
{
    public class WallController : Controller
    {
        IFriendRepository _friendRepository;
        IUserRepository _userRepository;
        IStatusRepository _statusRepository;
        IShareRepository _shareRepository;
        ApplicationDbContext _dbContext = new ApplicationDbContext();

        //
        // GET: /Wall/

        public ActionResult Index()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();

            this._shareRepository = new ShareRepository(this._dbContext);

            var listStatusIdShared = this._shareRepository.GetStatusIdShared(curUserId);

            this._statusRepository = new StatusRepository(this._dbContext);

            var listStatusViewModel = this._statusRepository.GetListStatusByRangeStatusId(listStatusIdShared);

            this._userRepository = new UserRepository(_dbContext);
            var curUser = this._userRepository.GetUserById(curUserId);
            ViewBag.curUser = curUser;

            return View(listStatusViewModel);
        }
	}
}