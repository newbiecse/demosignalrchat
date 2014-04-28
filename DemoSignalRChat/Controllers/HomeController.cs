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
    [Authorize]
    public class HomeController : Controller
    {
        IFriendRepository _friendRepository;
        IUserRepository _userRepository;
        IStatusRepository _statusRepository;
        ISearchRepository _searchRepository;
        ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();
            this._userRepository = new UserRepository(_dbContext);
            var curUser = this._userRepository.GetUserById(curUserId);
            ViewBag.curUser = curUser;

            return View(curUser);
        }

        public ActionResult Wall()
        {
            // get current user
            string curUserId = User.Identity.GetUserId();
            this._userRepository = new UserRepository(_dbContext);
            var curUser = this._userRepository.GetUserById(curUserId);
            ViewBag.curUser = curUser;

            return View(curUser);
        }


        public ActionResult StatusNewest()
        {
            string curUserId = User.Identity.GetUserId();

            this._userRepository = new UserRepository(_dbContext);
            var curUser = this._userRepository.GetUserById(curUserId);
            ViewBag.curUser = curUser;

            // get status newest
            this._statusRepository = new StatusRepository(this._dbContext);
            var listStatusNewest = this._statusRepository.GetListStatusNewest(curUserId);

            return View(listStatusNewest);
        }

        public ActionResult FriendList()
        {
            string curUserId = User.Identity.GetUserId();

            // get fiendlist
            this._friendRepository = new FriendRepository(_dbContext);
            var friendList = this._friendRepository.GetFriendList(curUserId);

            return View(friendList);
        }

        public ActionResult FriendListSuggest()
        {
            string curUserId = User.Identity.GetUserId();

            // friend sugest
            this._friendRepository = new FriendRepository(_dbContext);
            var friendListSugest = this._friendRepository.GetFriendListSugest(curUserId);
            ViewData["friendListSugest"] = friendListSugest;

            return View(friendListSugest);
        }

        public ActionResult GetNewFeeds(string userId)
        {
            INewFeedsRepository newFeedsRepository = new NewFeedsRepository(_dbContext);
            var newfeeds = newFeedsRepository.GetListNewFeeds(userId);
            return View(newfeeds);
        }


        public ActionResult GetComments(string statusid)
        {
            ICommentRepository commentRepository = new CommentRepository(_dbContext);
            var listCommented = commentRepository.GetCommentForStatus(statusid);
            return View(listCommented);
        }

        [HttpPost]
        public JsonResult GetPeopleLiked(string statusId)
        {
            ILikeRepository likeRepository = new LikeRepository(_dbContext);
            var listUserLiked = likeRepository.GetListUserLiked(statusId);
            return Json(listUserLiked, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetListFriendWaitAccept()
        {
            var userId = User.Identity.GetUserId();
            this._friendRepository = new FriendRepository(this._dbContext);

            var result = this._friendRepository.GetFriendListWaitAccept(userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Search(string paramSearch)
        {
            this._searchRepository = new SearchRepository(this._dbContext);

            var result = this._searchRepository.Search(paramSearch);
            //return View(this._searchRepository.Search(paramSearch));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                }
            }
        }


        //[HttpPost]
        //public ActionResult Upload()
        //{
        //    // get current user
        //    string curUserId = User.Identity.GetUserId();


        //    if (Request.Files.Count > 0)
        //    {
        //        var file = Request.Files[0];

        //        if (file != null && file.ContentLength > 0)
        //        {
        //            // create folder for user if it doesn't exist
        //            var pathExist = Server.MapPath(@"~/Uploads/Users/" + curUserId + "/Post/Images");
        //            if (!System.IO.Directory.Exists(pathExist))
        //            {
        //                System.IO.Directory.CreateDirectory(pathExist);
        //            }


        //            var fileName = Path.GetFileName(file.FileName);
        //            var path = Path.Combine(pathExist, string.Format(@"{0}-{1}", Guid.NewGuid(), fileName));
        //            file.SaveAs(path);
        //        }
        //    }

        //    return null;
        //}



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