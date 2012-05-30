using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmProduct.Model;
using System.ComponentModel.DataAnnotations;
using FarmProduct.Core;

namespace FarmProduct.Web.Models
{
    public class UserEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入登录名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入真实姓名")]
        public string RealName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [Range(1, 15)]
        public string Password { get; set; }

        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Range(1, 1000)]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "请选择角色")]
        public Role UserRole { get; set; }

        public List<Company> CompanyList { get; set; }

        public UserEditModel()
        {
            this.CompanyList = CompanySvc.LoadAllCompany();
        }

        public UserEditModel(User user)
            : this()
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.RealName = user.RealName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.Telephone = user.Telephone;
            this.CompanyId = user.Company.Id;
        }

    }
}