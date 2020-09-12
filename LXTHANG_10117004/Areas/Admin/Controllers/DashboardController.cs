using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private OrderBLL orderBLL = new OrderBLL();
        private ProductBLL productBLL = new ProductBLL();
        public ActionResult Index()
        {
            ViewBag.SoLuongSanPham = productBLL.SoLuongSanPham();
            //ViewBag.SoLuongDonhang = orderBLL.SoLuongDonhang();
            ViewBag.SoLuongDonHangChuaXacThuc = orderBLL.SoLuongDonHangChuaXacThuc();
            ViewBag.SoLuongDonHangDaThanhToan = orderBLL.SoLuongDonHangDaThanhToan();
            ViewBag.SoLuongDonHangDaHuy = orderBLL.SoLuongDonHangDaHuy();
            return View();
        }
    }
}