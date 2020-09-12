using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LXTHANG_10117004.Areas.Admin.Models;
using BLL;
using LXTHANG_10117004.Common;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private UserBLL userBLL = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                int flag = userBLL.Login1(loginModel.UserName, loginModel.Password);
                switch (flag)
                {
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại!");
                        break;
                    case 1:
                        ModelState.AddModelError("", "Tài khoản Client không được phép!");
                        break;
                    case 2:
                        var user = userBLL.GetUser(loginModel.UserName);
                        var userLogin = new AdminLoginViewModel();
                        userLogin.ID = user.ID;
                        userLogin.UserName = user.UserName;
                        Session.Add(CommonConstants.ADMIN_SESSION, userLogin);
                        return RedirectToAction("Index", "Dashboard");
                    case 3:
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                        break;
                }
            }
            return View("Index");
        }
    }
}