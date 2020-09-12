using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace LXTHANG_10117004.Controllers
{
    public class MenuController : Controller
    {
        private ProductBLL productBLL = new ProductBLL();
        private MenuBLL menuBLL = new MenuBLL();
        private ProductCategoryBLL productCategoryBLL = new ProductCategoryBLL();
        private PostCategoryBLL postCategoryBLL = new PostCategoryBLL();
        private PostBLL postBLL = new PostBLL();
        [ChildActionOnly]
        // GET: Menu
        public ActionResult LoadMenu()
        {
            if (Session["SS_Menu_Product"] == null)
            {
                Session["SS_Menu_Product"] = productBLL.GetAll();
                Session["SS_Menu_Submenu"] = productCategoryBLL.GetByParentID(2);
                Session["SS_Menu_PostCategory"] = postCategoryBLL.GetAll();
                Session["SS_Menu_Post"] = postBLL.GetAll();
            }
            ViewBag.Products = (List<Product>)Session["SS_Menu_Product"];
            ViewBag.Submenu = (List<ProductCategory>)Session["SS_Menu_Submenu"];
            ViewBag.PostCategory = (List<PostCategory>)Session["SS_Menu_PostCategory"];
            ViewBag.Post = (List<Post>)Session["SS_Menu_Post"];
            return PartialView(menuBLL.GetAll());
        }
    }
}