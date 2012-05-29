using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FarmProduct.Model
{
    public class User
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "请输入登录名")]
        [Range(5, 15)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入真实姓名")]
        public string RealName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [Range(5, 15)]
        public string Password { get; set; }

        [Range(5,50)]
        public string Email { get; set; }

        public int CompanyId { get; set; }

        public Role UserRole { get; set; }

        public bool IsDeleted { get; set; }

    }
}
