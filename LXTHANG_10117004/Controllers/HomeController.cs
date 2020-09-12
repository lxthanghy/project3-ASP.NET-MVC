using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DTO;

namespace LXTHANG_10117004.Controllers
{
    public class HomeController : Controller
    {
        private ProductBLL productBLL = new ProductBLL();
        // GET: Home
        public ActionResult Index()
        {

            if (Session["SS_Home_SanPhamHome"] == null)
            {
                Session["SS_Home_SanPhamHome"] = productBLL.SanPhamHome();
            }
            var spHome = (List<HomeProduct>)Session["SS_Home_SanPhamHome"];
            return View(spHome);
        }
    }
}