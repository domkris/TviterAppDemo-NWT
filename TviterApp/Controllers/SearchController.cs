using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TviterApp.Models;

namespace TviterApp.Controllers
{
    public class SearchController : Controller
    {
        
        private ApplicationDbContext db;
        public SearchController()
        {
            db = new ApplicationDbContext();
        }
        

        public ActionResult OtherUser(string otherUserName)
        {
            return View();
        }
        public JsonResult GetOtherUser(string otherUserName)
        {
            ApplicationUser user = db.Users.Where(p => p.UserName == otherUserName).SingleOrDefault();
            return Json(new { user = user }, JsonRequestBehavior.AllowGet); 

        }
        public JsonResult GetPostsByOtherUser(string userName)
        {
            if (userName == null)
            {
                return null;
            }
            else {
                var otherUser = db.Users.Where(k => k.Email == userName).FirstOrDefault();
                List<Post> ListOfPosts = db.Posts.Where(a => a.ApplicationUserID == otherUser.Id).ToList();
                return Json(new { listOfAllPosts = ListOfPosts }, JsonRequestBehavior.AllowGet);

            }
            
        }
    }
}