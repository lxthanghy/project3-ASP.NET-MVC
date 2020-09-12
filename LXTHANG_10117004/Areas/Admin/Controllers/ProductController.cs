using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using BLL;
using PagedList;
using DTO;
using Utility;
using LXTHANG_10117004.Areas.Admin.Models;
using System.Globalization;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private ProductBLL productBLL = new ProductBLL();
        private SupplierBLL supplierBLL = new SupplierBLL();
        private ProductCategoryBLL productCategoryBLL = new ProductCategoryBLL();
        // GET: Admin/Product
        public ActionResult Index(int? page, int? size, bool? loadPage, string searchName)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 10;
            bool LoadPage = loadPage ?? false;
            ViewBag.PageSize = PageSize;
            ViewBag.LoadPage = LoadPage;
            ViewBag.SearchName = string.IsNullOrEmpty(searchName) ? "" : searchName;
            ViewBag.lstSupplier = supplierBLL.LoadOption();
            ViewBag.lstProductCategory = productCategoryBLL.LoadOption();
            List<Product> model = null;
            if (loadPage == true || Session["SS_LoadProduct"] == null)
            {
                LoadPage = false;
                ViewBag.LoadPage = LoadPage;
                model = productBLL.GetAll();
                Session["SS_LoadProduct"] = model;
            }
            else
            {
                model = (List<Product>)Session["SS_LoadProduct"];
            }
            if (!string.IsNullOrEmpty(searchName))
                model = model.Where(x => (x.Name.ToLower().Contains(searchName.ToLower()))).ToList();
            ViewBag.Total = model.Count;
            ViewBag.Page = PageNumber;
            return View(model.ToPagedList(PageNumber, PageSize));
        }
        public JsonResult GetByIdInfo(int id)
        {
            try
            {
                var product = productBLL.GetById(id);
                var x = new ProductViewModel();
                if (product != null)
                {
                    x.ID = product.ID;
                    x.Name = product.Name;
                    x.Image = product.Image;
                    var lstImage = Tools.LoadMoreImages(product.MoreImages);
                    x.Description = string.IsNullOrEmpty(product.Description) ? "Không có" : product.Description;
                    x.Frame = product.Frame;
                    x.Rims = product.Rims;
                    x.Tires = product.Tires;
                    x.Weight = product.Weight;
                    x.Weightlimit = product.Weightlimit;
                    x.StrPrice = string.Format("{0:#,##0}", product.Price) + " VNĐ";
                    x.StrPromotion = product.Promotion > 0 ? Convert.ToString(product.Promotion) + "%" : "Không có";
                    x.Quantity = product.Quantity;
                    x.StrCategory = productCategoryBLL.GetName(product.CategoryID);
                    x.StrSupplier = supplierBLL.GetName(product.SupplierID);
                    x.CreatedBy = string.IsNullOrEmpty(product.CreatedBy) ? "Không có" : product.CreatedBy;
                    x.CreatedDate = product.CreatedDate.ToString();
                    x.ModifiedBy = string.IsNullOrEmpty(product.ModifiedBy) ? "Không có" : product.ModifiedBy;
                    x.ModifiedDate = string.IsNullOrEmpty(product.ModifiedDate.ToString()) ? "Không có" : product.ModifiedDate.ToString();
                    x.ViewCount = product.ViewCount;
                    return Json(new
                    {
                        obj = x,
                        lstImage = lstImage
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetByIdEdit(int id)
        {
            try
            {
                var x = new ProductViewModel();
                var y = productBLL.GetById(id);
                if (y != null)
                {
                    x.ID = id;
                    x.Name = y.Name;
                    x.Image = y.Image;
                    var lstImage = Tools.LoadMoreImages(y.MoreImages);
                    x.Description = y.Description;
                    x.Frame = y.Frame;
                    x.Rims = y.Rims;
                    x.Tires = y.Tires;
                    x.Weight = y.Weight;
                    x.Weightlimit = y.Weightlimit;
                    x.Price = y.Price;
                    x.Promotion = y.Promotion;
                    x.Quantity = y.Quantity;
                    x.CategoryID = y.CategoryID;
                    x.SupplierID = y.SupplierID;
                    x.CreatedBy = y.CreatedBy;
                    x.CreatedDate = y.CreatedDate.ToString();
                    x.ModifiedBy = y.ModifiedBy;
                    x.ModifiedDate = y.ModifiedDate.ToString();
                    x.Status = y.Status;
                    x.HomeFlag = y.HomeFlag;
                    x.HotFlag = y.HotFlag;
                    x.ViewCount = y.ViewCount;
                    return Json(new
                    {
                        obj = x,
                        lstImage = lstImage
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult SaveProduct(string product)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Product p = serializer.Deserialize<Product>(product);
            try
            {
                if (p != null)
                {
                    if (p.ID == 0)
                        productBLL.AddProduct(p);
                    else
                        productBLL.EditProduct(p);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public JsonResult DeleteProduct(int id)
        {
            var result = productBLL.DeleteProduct(id);
            return Json(result);
        }
    }
}