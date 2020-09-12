using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Models
{
    public class RegisterModel
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

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp!")]
        [Required(ErrorMessage = "Xác nhận mật khẩu!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Nhập tên người dùng!")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Nhập địa chỉ!")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Nhập email!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Nhập điện thoại!")]
        public string Mobile { get; set; }
    }
}