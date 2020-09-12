using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;
using Utility;

namespace LXTHANG_10117004.Controllers
{
    public class PostController : Controller
    {
        private PostBLL postBLL = new PostBLL();
        // GET: Post
        public ActionResult Index(int id)
        {
            var post = postBLL.GetByID(id);
            return View(post);
        }
    }
}