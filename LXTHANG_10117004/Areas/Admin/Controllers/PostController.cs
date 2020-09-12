using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using DTO;
using PagedList;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private PostBLL postBLL = new PostBLL();
        private PostCategoryBLL postCategoryBLL = new PostCategoryBLL();
        // GET: Admin/Post
        public ActionResult Index()
        {
            var data = postBLL.GetAll();
            return View(data);
        }
        [HttpGet]
        public ActionResult ThemTinTuc()
        {
            ViewBag.lstPostCategory = postCategoryBLL.LoadOption();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddPost(string post)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Post p = serializer.Deserialize<Post>(post);
            var flag = postBLL.ThemTinTuc(p);
            return Json(flag);
        }
        [HttpGet]
        public ActionResult SuaTinTuc(int id)
        {
            var post = postBLL.GetByID(id);
            ViewBag.lstPostCategory = postCategoryBLL.LoadOption();
            return View(post);
        }
        public JsonResult GetContent(int id)
        {
            var post = postBLL.GetByID(id);
            return Json(post.Content, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditPost(string post)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Post p = serializer.Deserialize<Post>(post);
            var flag = postBLL.SuaTinTuc(p);
            return Json(flag);
        }
        [HttpPost]
        public JsonResult DeletePost(int id)
        {
            var flag = postBLL.XoaTinTuc(id);
            return Json(flag);
        }
    }
}