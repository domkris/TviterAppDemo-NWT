using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TviterApp.Models;

namespace TviterApp.Controllers
{ 
    // moguce rješenje problema (specificiraj columns ka al trebaju mi sve..) sa "A circular reference was detected while serializing an object of type 'SubSonic.Schema .DatabaseColumn'.
    /*
            var list = JsonConvert.SerializeObject(model,
            Formatting.None,
            new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        });
        {
    */
    [Authorize]
    public class PostController : Controller
    {
        private ApplicationDbContext db;
        public PostController() {
            db = new ApplicationDbContext();
        }

        // GET ALL POSTS

        public JsonResult GetAllPosts() {
            //IQueryable<Post> AllPosts = db.Posts;
            List<Post> AllPosts = db.Posts.ToList();
           
            //List<Post> ListOfPost = AllPosts.ToList();
            return Json(new { listOfAllPosts = AllPosts}, JsonRequestBehavior.AllowGet);
        }

        // GET POSTS BY USER
        public JsonResult GetPostsByUser(string userName)
        {
            if (userName == null)
            {

                var currentUser = db.Users.Where(k => k.Email == User.Identity.Name).FirstOrDefault();
                var followers = db.Follow.Where(x => x.CurrentUserId == currentUser.Id).Select(temp => new { temp.OtherUserId }).ToList();
                List<Post> ListOfPosts = db.Posts.Where(a => a.ApplicationUserID == currentUser.Id).ToList();
                foreach (var follower in followers)
                {
                    var newFollower = db.Users.Where(x=> x.UserName == follower.OtherUserId).SingleOrDefault();
                    var newFollowerId = newFollower.Id; 
                    List<Post> ListOfFollowerPosts = db.Posts.Where(a=>a.ApplicationUserID == newFollowerId).ToList();
                    foreach (var post in ListOfFollowerPosts) {
                        ListOfPosts.Add(post);
                    }
                }
                if (ListOfPosts == null) {
                    return Json(new { });
                }
                foreach (var post in ListOfPosts)
                {
                    var likes = db.Likes.Where(x => x.PostId == post.Id).Count();
                    var bljaks = db.Bljaks.Where(x => x.PostId == post.Id).Count();
                    post.numOfLikes = likes;
                    post.numOfBljaks = bljaks;
                }
                return Json(new { listOfAllPosts = ListOfPosts }, JsonRequestBehavior.AllowGet);
            }
            else {
                var otherUser = db.Users.Where(k => k.Email == userName).FirstOrDefault();
                List<Post> ListOfPosts = db.Posts.Where(a => a.ApplicationUserID == otherUser.Id).ToList();
                foreach (var post in ListOfPosts)
                {
                    var likes = db.Likes.Where(x => x.PostId == post.Id).Count();
                    var bljaks = db.Bljaks.Where(x => x.PostId == post.Id).Count();
                    post.numOfLikes = likes;
                    post.numOfBljaks = bljaks;
                }
                return Json(new { listOfAllPosts = ListOfPosts }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetLikesByPost(int PostId)
        {
            var likes = db.Likes.Where(x => x.PostId == PostId).ToString();
            return Json(new { likes = likes}); 
        }
        public JsonResult GetBljaksByPost(int PostId)
        {
            var bljaks = db.Bljaks.Where(x => x.PostId == PostId).ToString();
            return Json(new { bljaks = bljaks});
        }

        // POST post or ADD post
        [Authorize]
        public JsonResult AddPost(Post post) {
            string emailOfUser = User.Identity.Name;
            var currentUser = db.Users.Where(k => k.Email == User.Identity.Name).FirstOrDefault();
            post.ApplicationUserID = currentUser.Id;
            post.ApplicationUserName = currentUser.UserName;
            post.CreatedAt = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            return Json( new { status = "Post added successfully"});
        }

        // Update post 
        public JsonResult UpdatePost(int PostId, string PostDescription )
        {
            Post post = db.Posts.Where(x=> x.Id == PostId).SingleOrDefault();
            post.PostDescription = PostDescription;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { status = "Post updated successfully" });
        }
        public JsonResult LikePost(int PostId)
        {
            Like like = db.Likes.Where(x => x.PostId == PostId && x.ApplicationUserID == User.Identity.Name ).FirstOrDefault();
            if (like == null)
            {
                Like newLike = new Like();
                newLike.PostId = PostId;
                newLike.ApplicationUserID = User.Identity.Name;
                db.Likes.Add(newLike);
                db.SaveChanges();
                return Json(new { status = "Post liked successfully" });
            }
            else {
                db.Likes.Remove(like);
                db.SaveChanges();
                return Json(new { status = "Post unliked successfully" });
            }
            
            
        }
        public JsonResult BljakPost(int PostId)
        {

            Bljak bljak = db.Bljaks.Where(x => x.PostId == PostId && x.ApplicationUserID == User.Identity.Name).FirstOrDefault();
            if (bljak == null)
            {
                Bljak newBljak = new Bljak();
                newBljak.PostId = PostId;
                newBljak.ApplicationUserID = User.Identity.Name;
                db.Bljaks.Add(newBljak);
                db.SaveChanges();
                return Json(new { status = "Post bljaked successfully" });
            }
            else
            {
                db.Bljaks.Remove(bljak);
                db.SaveChanges();
                return Json(new { status = "Post unbljaked successfully" });
            }
        }

        // Delete post 
        public JsonResult DeletePost(int PostId)
        {
            Post post = db.Posts.Where(a => a.Id == PostId).SingleOrDefault();
            db.Posts.Remove(post);
            db.SaveChanges();
            return Json(new { status = "Post deleted successfully" });
        }
    }
}