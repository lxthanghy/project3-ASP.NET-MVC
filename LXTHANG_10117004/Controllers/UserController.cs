using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;
using LXTHANG_10117004.Models;
using LXTHANG_10117004.Common;
using CaptchaMvc.HtmlHelpers;

namespace LXTHANG_10117004.Controllers
{
    public class UserController : Controller
    {
        private UserBLL userBLL = new UserBLL();
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ReturnUrl = ReturnUrl;
                int flag = userBLL.Login(loginModel.UserName, loginModel.Password);
                switch (flag)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại!");
                        break;
                    case 1:
                        ModelState.AddModelError("", "Tài khoản admin không được phép!");
                        break;
                    case 2:
                        ModelState.AddModelError("", "Tài khoản bị khóa!");
                        break;
                    case 3:
                        var user = userBLL.GetUser(loginModel.UserName);
                        var userLogin = new UserLoginModel();
                        userLogin.ID = user.ID;
                        userLogin.UserName = user.UserName;
                        Session.Add(CommonConstants.USER_SESSION, userLogin);
                        if (!string.IsNullOrEmpty(ReturnUrl))
                            return Redirect(ReturnUrl);
                        else
                            return Redirect("/thong-tin-tai-khoan");
                    case 4:
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                        break;
                }
            }
            return View("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            //Kiểm tra dữ liệu và capcha
            if (ModelState.IsValid)
            {
                if (userBLL.CheckUserName(register.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (userBLL.CheckEmail(register.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (!this.IsCaptchaValid(""))
                {
                    ModelState.AddModelError("", "Mã Capcha không đúng!");
                }
                else
                {
                    bool result = userBLL.Register(register.UserName, register.Password, register.Name, register.Address, register.Email, register.Mobile);
                    if (result)
                    {
                        return RedirectToAction("Login", "User");
                    }
                    else
                        ModelState.AddModelError("", "Đăng ký thất bại!");
                }
            }
            return View(register);
        }
        public ActionResult ViewMyUser()
        {
            var userLogin = (UserLoginModel)Session[CommonConstants.USER_SESSION];
            var user = userBLL.GetUser(userLogin.UserName);
            return View(user);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Login", "User");
        }
    }
}