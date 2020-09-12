using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace LXTHANG_10117004.Controllers
{
    public class ContactController : Controller
    {
        private ContactBLL contactBLL = new ContactBLL();
        // GET: Contact
        public ActionResult Index()
        {
            return View(contactBLL.GetContact());
        }
    }
}