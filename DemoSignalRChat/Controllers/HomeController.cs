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

            return View(user);
        }

        //[HttpPost]
        public ActionResult Upload()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();


            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    // create folder for user if it doesn't exist
                    var pathExist = Server.MapPath(@"~/Uploads/Users/" + curUserId + "/Post/Images");
                    if (!System.IO.Directory.Exists(pathExist))
                    {
                        System.IO.Directory.CreateDirectory(pathExist);
                    }


                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(pathExist, string.Format(@"{0}-{1}", Guid.NewGuid(), fileName));
                    file.SaveAs(path);
                }
            }

            return null;
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