using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL;
using DTO;
using PagedList;
using LXTHANG_10117004.Areas.Admin.Models;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class PostCategoryController : BaseController
    {
        private PostCategoryBLL postCategoryBLL = new PostCategoryBLL();
        // GET: Admin/PostCategory
        public ActionResult Index()
        {
            var data = postCategoryBLL.GetAll();
            return View(data);
        }
        [HttpGet]
        public ActionResult ThemLoaiTinTuc()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddPostCategory(string postCategory)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PostCategory pc = serializer.Deserialize<PostCategory>(postCategory);
            var flag = postCategoryBLL.ThemLoaiTinTuc(pc);
            return Json(flag);
        }
        [HttpPost]
        public JsonResult DeletePostCategory(int id)
        {
            var flag = postCategoryBLL.XoaLoaiTinTuc(id);
            return Json(flag);
        }
        [HttpGet]
        public ActionResult SuaLoaiTinTuc(int id)
        {
            var postCategory = postCategoryBLL.GetByID(id);
            return View(postCategory);
        }
        public JsonResult GetDescription(int id)
        {
            var postCategory = postCategoryBLL.GetByID(id);
            return Json(postCategory.Description, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditPostCategory(string postCategory)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PostCategory pc = serializer.Deserialize<PostCategory>(postCategory);
            var flag = postCategoryBLL.SuaLoaiTinTuc(pc);
            return Json(flag);
        }
    }
}