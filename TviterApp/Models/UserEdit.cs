using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TviterApp.Models
{
    public class UserEdit
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string UserDescription { get; internal set; }
    }
}