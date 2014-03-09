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
        IFriendRepository _friendRepository;
        IUserRepository _userRepository;
        IStatusRepository _statusRepository;
        ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();
            this._userRepository = new UserRepository(_dbContext);
            var curUser = this._userRepository.GetUserById(curUserId);

            // get fiendlist
            this._friendRepository = new FriendRepository(_dbContext);
            var friendList = this._friendRepository.GetFriendList(curUserId);

            // store friendlist
            ViewData["friendList"] = friendList;

            // get status newest
            this._statusRepository = new StatusRepository(this._dbContext);
            var listStatusNewest = this._statusRepository.GetListStatusNewest(curUserId);

            return View(curUser);
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