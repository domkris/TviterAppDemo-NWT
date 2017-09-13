using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TviterApp.Models;

namespace TviterApp.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext context;
        //private ApplicationUserManager _userManager;

        public ProfileController()
        {
            context = new ApplicationDbContext();
            
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(string Email,string CustomUsername, string UserDescription) { 

            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var id = User.Identity.GetUserId();
            var currentUser = manager.FindById(id);
            if (Email != null) {
                currentUser.Email = Email;
            }
            if (CustomUsername != null)
            {
                currentUser.CustomUsername = CustomUsername;
            }
            if (UserDescription != null)
            {
                currentUser.UserDescription = UserDescription;
            }
           
            

            await manager.UpdateAsync(currentUser);
            var ctx = store.Context;
            ctx.SaveChanges();
            TempData["msg"] = "Profile Changes Saved !";
            return RedirectToAction("ListUser");

        }
 
    }
}