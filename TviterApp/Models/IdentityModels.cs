using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
using System.Web;

namespace TviterApp.Models
{

    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string PostDescription { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        public  string ApplicationUserName { get; set; }
        
        [NotMapped]
        public int numOfLikes { get; set; }
        [NotMapped]
        public int numOfBljaks { get; set; }
        public virtual string ApplicationUserID { get; set; }
        


    }
    public class Follow
    {
        [Key, Column(Order = 0)]
        public string CurrentUserId { get; set; }

        [Key, Column(Order = 1)]
        public string OtherUserId { get; set; }
    }

    public class UploadedFiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }

    }

    public class Like {
        [Key, Column(Order = 0)]
        public int PostId { get; set; }

        [Key, Column(Order = 1)]
        public string ApplicationUserID { get; set; }
    }
    public class Bljak
    {
        [Key, Column(Order = 0)]
        public int PostId { get; set; }

        [Key, Column(Order = 1)]
        public string ApplicationUserID { get; set; }
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
        public string CustomUsername { get; set; }
        public string imagePath { get; set; }

        


        // maknio sam virutal ovdi doli radi ef proxyija koji mi radi lazy loading sa virtual
        public ICollection<Post> Posts { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Bljak> Bljaks { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // ovo je fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // one-to-zero or one relationship between ApplicationUser and Post
            // UserId column in Post table will be foreign key
            modelBuilder.Entity<Post>()
            .Property(c => c.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UploadedFiles>()
            .Property(c => c.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            /*
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(m => m.Posts)
                .WithRequired(m => m.ApplicationUser)
                .Map(p => p.MapKey("UserId"));
            */
        }

    }
}