using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;
using PagedList;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private ProductCategoryBLL productCategoryBLL = new ProductCategoryBLL();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            return View(productCategoryBLL.GetAll());
        }
    }
}