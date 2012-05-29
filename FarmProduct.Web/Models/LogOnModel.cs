using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Web.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "保持登录?")]
        public bool RememberMe { get; set; }
    }
}