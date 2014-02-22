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

            //var client = new WebClient();
            //string html = client.DownloadString("http://www.aao.hcmut.edu.vn/");
            //Document doc = NSoup.NSoupClient.Parse(html);

            //Elements imgs = doc.Select("img");

            //List<Img> images = new List<Img>();
            //foreach (var i in imgs)
            //{
            //    string imgWidth = i.Attr("Width");
            //    int width = 0;
            //    Int32.TryParse(imgWidth, out width);

            //    string imgHeight = i.Attr("Height");
            //    int height = 0;
            //    Int32.TryParse(imgHeight, out height);

            //    images.Add(new Img { Src = i.Attr("src"), Width = width, Height = height });
            //}

            //List<Img> imagesSorted = (from i in images
            //                          select i).OrderByDescending(i => i.Height * i.Width).ToList();

            //ViewBag.imgSrcs = imagesSorted;

            //Document doc = NSoup.NSoupClient.Parse(responseString);
            //string imgSrc = doc.Select("");

            //int x = 9;


            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult PostLink(string Content)
        {
            //var client = new WebClient();
            ////client.DownloadData
            //string html = client.DownloadString(Content);
            //Document doc = NSoup.NSoupClient.Parse(html);

            //Elements imgs = doc.Select("img");

            //List<Img> images = new List<Img>();
            //foreach (var i in imgs)
            //{
            //    images.Add(new Img(i.Attr("Height"), i.Attr("Width"), i.Attr("src")));
            //}

            //List<Img> imagesSorted = (from i in images
            //                          select i).OrderByDescending(i => i.Height * i.Width).ToList();

            //var src = imagesSorted.First().Src;

            //return Json(src, JsonRequestBehavior.AllowGet);
            return Json("");
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