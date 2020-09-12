using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace LXTHANG_10117004.Controllers
{
    public class FeedBackController : Controller
    {
        private FeedbackBLL feedbackBLL = new FeedbackBLL();
        // GET: FeedBack
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AddFeedback(string name, string email, string address, string phone, string message)
        {
            bool result = feedbackBLL.AddFeedback(name, email, address, phone, message);
            return Json(result);
        }
    }
}