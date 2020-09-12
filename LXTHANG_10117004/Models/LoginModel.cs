using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Models
{
    [Serializable]
    public class LoginModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Nhập tên đăng nhập!")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự!")]
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        public string Password { get; set; }

        [Display(Name = "Nhớ mật khẩu")]
        public bool RememberMe { get; set; }
    }
}