using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Nhập tên đăng nhập!")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự!")]
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }
    }
}