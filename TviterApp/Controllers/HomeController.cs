using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TviterApp.Models;

namespace TviterApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private ApplicationDbContext db;

        public HomeController() {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
           
            var emailOfUser = User.Identity.Name;
           
            ApplicationUser user = new ApplicationUser();
            user = db.Users.Where(p => p.UserName == emailOfUser).SingleOrDefault();
            ViewBag.user = user;
            return View();
        }

        public JsonResult GetUser(string userName) {
            if (userName == null)
            {
                var UserId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(a => a.Id == UserId).SingleOrDefault();

                return Json(new { user = user }, JsonRequestBehavior.AllowGet);
            }
            else {
                ApplicationUser user = db.Users.Where(a => a.UserName == userName).SingleOrDefault();
                return Json(new { user = user }, JsonRequestBehavior.AllowGet);
            }
            

        }
        public JsonResult GetUserImage(string userName)
        {
            ApplicationUser user = db.Users.Where(a => a.UserName == userName).SingleOrDefault();

            return Json(new { image = user.imagePath }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Follow(string otherUserName)
        {
            var currentUserId = User.Identity.GetUserId();

            if(db.Follow.Count(x => x.CurrentUserId == currentUserId && x.OtherUserId == otherUserName) == 0)
            {
                Follow newFollow = new Follow();
                newFollow.CurrentUserId = currentUserId;
                newFollow.OtherUserId = otherUserName;
                db.Follow.Add(newFollow);
                db.SaveChanges();
                return Json(new { status = "Followed successfully" });
            }
            else
            {
                Follow follow = db.Follow.Where(x => x.CurrentUserId == currentUserId && x.OtherUserId == otherUserName).Single();
                db.Follow.Remove(follow);
                db.SaveChanges();
                return Json(new { status = "Unfollowed successfully" });
            }
            
           

        }
        public JsonResult GetFollowers()
        {
           
            var userId = User.Identity.GetUserId();
            var followers = db.Follow.Where(x => x.CurrentUserId == userId).Select(temp => new { temp.OtherUserId }).ToList();
            return Json(new { followers = followers }, JsonRequestBehavior.AllowGet);
          
        }
        public JsonResult GetOtherFollowers(string userName)
        {
            ApplicationUser user = db.Users.Where(a => a.UserName == userName).SingleOrDefault();
            var userId = user.Id;
            var followers = db.Follow.Where(x => x.CurrentUserId == userId).Select(temp => new { temp.OtherUserId }).ToList();
            return Json(new { followers = followers }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllUsers()
        {
            var Users = db.Users.Select(x => new {
                UserName = x.UserName
            }).ToList();
            return Json(new { users = Users }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> EditUser(UserEdit model)
        {

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindById(User.Identity.GetUserId());
            currentUser.Email = model.Email;
            currentUser.UserDescription = model.UserDescription;

            await manager.UpdateAsync(currentUser);
            var ctx = store.Context;
            ctx.SaveChanges();
            TempData["msg"] = "Profile Changes Saved !";
            return RedirectToAction("Index");

        }
        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public JsonResult SaveFiles(string description)
        {
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                // dodavanje slike za korisnika
                var UserId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Where(a => a.Id == UserId).SingleOrDefault();
                user.imagePath = fileName;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                int size = file.ContentLength;


                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"),fileName));
                    UploadedFiles f = new UploadedFiles
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                        FileSize = size
                    };
                    db.UploadedFiles.Add(f);
                    db.SaveChanges();
                    Message = "File uploaded successfully!";
                    flag = true;
                }
                catch(Exception)
                {
                    Message = "File upload failed! Try again";
                }
            }


            return new JsonResult { Data = new { Message = Message, Status = flag} };
        }

        public ActionResult EditProfile() {

            return View();
        }


        public async Task<ActionResult> UserDeleteConfirmed()
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var id = User.Identity.GetUserId();
            var currentUser = manager.FindById(id);

            var result = await manager.DeleteAsync(currentUser);
            if (result.Succeeded)
            {
                TempData["UserDeleted"] = "User Successfully Deleted";
                FormsAuthentication.SignOut();
                Roles.DeleteCookie();
                Session.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["UserDeleted"] = "Error Deleting User";
                return RedirectToAction("Index");
            }
        }
       
    }  
}