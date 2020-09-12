using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using BLL;
using DTO;
using Utility;
using PagedList;

namespace LXTHANG_10117004.Controllers
{
    public class ProductController : Controller
    {
        private ProductBLL productBLL = new ProductBLL();
        // GET: Product
        public ActionResult Index(int? page, int? size, string searchName, int? sort)
        {
            return View();
        }
        //Trang danh sách toàn bộ sản phẩm
        public ActionResult Shop(int? page, int? size, string searchName, int? sort)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 12;
            int Sort = sort ?? 0;
            ViewBag.PageSize = PageSize;
            ViewBag.Sort = Sort;
            ViewBag.Title = "Xe Đạp";
            ViewBag.SearchName = string.IsNullOrWhiteSpace(searchName) ? null : searchName;
            if (Session["SS_Shop"] == null)
            {
                Session["SS_Shop"] = productBLL.GetAllHomeProduct();
            }
            var data = (List<HomeProduct>)Session["SS_Shop"];
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.ToLower();
                data = data.Where(x => x.Name.ToLower().Contains(searchName) || x.SupplierName.ToLower().Contains(searchName)).ToList();
            }
            var dataSort = new List<HomeProduct>();
            switch (Sort)
            {
                case 0:
                    dataSort = data;
                    break;
                case 1:
                    dataSort = data.OrderBy(x => x.NewPrice).ToList();
                    break;
                case 2:
                    dataSort = data.OrderBy(x => x.Name).ToList();
                    break;
            }
            ViewBag.Total = dataSort.Count;
            ViewBag.Page = PageNumber;
            return View(dataSort.ToPagedList(PageNumber, PageSize));
        }
        //Danh sách sản phẩm theo loại
        public ActionResult CategoryShop(int id, int? page, int? size, string searchName, int? sort)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 12;
            int Sort = sort ?? 0;
            ViewBag.ID = id;
            ViewBag.PageSize = PageSize;
            ViewBag.Sort = Sort;
            ViewBag.SearchName = string.IsNullOrWhiteSpace(searchName) ? null : searchName;
            if (Session["SS_CategoryShop_" + id] == null)
            {
                Session["SS_CategoryShop_" + id] = productBLL.LaySanPhamTheoLoai(id);
            }
            var data = (List<HomeProduct>)Session["SS_CategoryShop_" + id];
            ViewBag.Title = data[0].CategoryName;
            ViewBag.Alias = data[0].CategoryAlias;
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.ToLower();
                data = data.Where(x => x.Name.ToLower().Contains(searchName) || x.SupplierName.ToLower().Contains(searchName)).ToList();
            }
            var dataSort = new List<HomeProduct>();
            switch (Sort)
            {
                case 0:
                    dataSort = data;
                    break;
                case 1:
                    dataSort = data.OrderBy(x => x.NewPrice).ToList();
                    break;
                case 2:
                    dataSort = data.OrderBy(x => x.Name).ToList();
                    break;
            }
            ViewBag.Total = dataSort.Count;
            ViewBag.Page = PageNumber;
            return View(dataSort.ToPagedList(PageNumber, PageSize));
        }
        //Danh sách sản phẩm theo loại và nhà cung cấp
        public ActionResult CategorySupplierShop(int idCate, int idSupp, int? page, int? size, string searchName, int? sort)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 12;
            int Sort = sort ?? 0;
            ViewBag.IDCate = idCate;
            ViewBag.IDSupp = idSupp;
            ViewBag.PageSize = PageSize;
            ViewBag.Sort = Sort;
            ViewBag.SearchName = string.IsNullOrWhiteSpace(searchName) ? null : searchName;
            if (Session["SS_CategorySupplierShop_" + idCate + "_" + idSupp] == null)
            {
                Session["SS_CategorySupplierShop_" + idCate + "_" + idSupp] = productBLL.LaySanPhamTheoLoaiVaNhaCC(idCate, idSupp);
            }
            var data = (List<HomeProduct>)Session["SS_CategorySupplierShop_" + idCate + "_" + idSupp];
            ViewBag.Title = data[0].CategoryName + " " + data[0].SupplierName;
            ViewBag.CategoryAlias = data[0].CategoryAlias;
            ViewBag.SupplierName = data[0].SupplierName;
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.ToLower();
                data = data.Where(x => x.Name.ToLower().Contains(searchName) || x.SupplierName.ToLower().Contains(searchName)).ToList();
            }
            var dataSort = new List<HomeProduct>();
            switch (Sort)
            {
                case 0:
                    dataSort = data;
                    break;
                case 1:
                    dataSort = data.OrderBy(x => x.NewPrice).ToList();
                    break;
                case 2:
                    dataSort = data.OrderBy(x => x.Name).ToList();
                    break;
            }
            ViewBag.Total = dataSort.Count;
            ViewBag.Page = PageNumber;

            return View(dataSort.ToPagedList(PageNumber, PageSize));
        }
        //Danh sách sản phẩm mới nhất home
        [ChildActionOnly]
        public ActionResult SanPhamMoiNhatHome()
        {
            if (Session["SS_Home_SanPhamMoiNhatHome"] == null)
            {
                Session["SS_Home_SanPhamMoiNhatHome"] = productBLL.SanPhamMoiNhatHome();
            }
            var spMoiNhatHome = (List<HomeProduct>)Session["SS_Home_SanPhamMoiNhatHome"];
            return PartialView(spMoiNhatHome);
        }
        //Xem chi tiết sản phẩm
        public ActionResult ViewDetail(int id)
        {
            Product p = productBLL.GetById(id);
            Session["SS_ViewDetail_RelatedProduct"] = null;
            if (Session["SS_ViewDetail_RelatedProduct"] == null)
            {
                Session["SS_ViewDetail_RelatedProduct"] = productBLL.SanPhamLienQuan(id, p.SupplierID);
            }
            ViewBag.RelatedProduct = (List<HomeProduct>)Session["SS_ViewDetail_RelatedProduct"];
            ViewBag.MoreImages = Tools.LoadMoreImages(p.MoreImages);
            return View(p);
        }
        //Lấy danh sách sản phẩm đang sales
        public ActionResult SaleShop(int? page, int? size, string searchName, int? sort)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 12;
            int Sort = sort ?? 0;
            ViewBag.PageSize = PageSize;
            ViewBag.Sort = Sort;
            ViewBag.SearchName = string.IsNullOrWhiteSpace(searchName) ? null : searchName;
            ViewBag.Title = "Sản phẩm sale";
            if (Session["SS_SaleShop"] == null)
            {
                Session["SS_SaleShop"] = productBLL.LaySanPhamSale();
            }
            var data = (List<HomeProduct>)Session["SS_SaleShop"];
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.ToLower();
                data = data.Where(x => x.Name.ToLower().Contains(searchName) || x.SupplierName.ToLower().Contains(searchName)).ToList();
            }
            var dataSort = new List<HomeProduct>();
            switch (Sort)
            {
                case 0:
                    dataSort = data;
                    break;
                case 1:
                    dataSort = data.OrderBy(x => x.NewPrice).ToList();
                    break;
                case 2:
                    dataSort = data.OrderBy(x => x.Name).ToList();
                    break;
            }
            ViewBag.Total = dataSort.Count;
            ViewBag.Page = PageNumber;
            return View(dataSort.ToPagedList(PageNumber, PageSize));
        }
        //Tìm kiếm sản phẩm
        public ActionResult SearchProduct(string searchName)
        {
            ViewBag.SearchName = searchName;
            ViewBag.Title = "Tìm kiếm";
            if (string.IsNullOrWhiteSpace(searchName))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var data = productBLL.SearchByName(searchName);
                return View(data);
            }
        }
        public JsonResult SearchKeyup(string search)
        {
            var data = productBLL.SearchByName(search);
            if (!string.IsNullOrWhiteSpace(search))
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}