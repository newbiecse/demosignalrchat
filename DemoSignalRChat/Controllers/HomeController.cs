using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoSignalRChat.Models;
using DemoSignalRChat.ViewModels;
using DemoSignalRChat.DAL;
using Microsoft.AspNet.Identity;
using System.Net;
using NSoup.Nodes;
using NSoup;

namespace DemoSignalRChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();

            ApplicationUser tUser = db.Users.Find(curUserId);

            //ApplicationUser tUser = (ApplicationUser)Session["user"];
            var user = new UserViewModel { UserId = tUser.Id, UserName = tUser.UserName };

            // get friend list
            var friendList = new FriendListViewModel(user.UserId);
            ViewBag.FriendList = friendList.FriendList;

            string responseString;
            using (var client = new WebClient())
            {
                responseString = client.DownloadString("http://vnexpress.net/");
            }

            //Document doc = NSoup.NSoupClient.Parse(responseString);
            //string imgSrc = doc.Select("");

            //int x = 9;

            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}