using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using DTO;
using Utility;

namespace LXTHANG_10117004.Controllers
{
    public class PostCategoryController : Controller
    {
        private PostCategoryBLL postCategoryBLL = new PostCategoryBLL();
        // GET: PostCategory
        public ActionResult Index()
        {
            return View();
        }
    }
}