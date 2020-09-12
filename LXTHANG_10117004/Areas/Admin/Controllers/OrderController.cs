using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using PagedList;
using LXTHANG_10117004.Areas.Admin.Models;

namespace LXTHANG_10117004.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private OrderBLL orderBLL = new OrderBLL();
        private OrderDetail orderDetail = new OrderDetail();
        private ProductBLL productBLL = new ProductBLL();
        // GET: Admin/Order
        public ActionResult Index(int? page, int? size)
        {
            int PageNumber = page ?? 1;
            int PageSize = size ?? 5;
            ViewBag.PageSize = PageSize;
            var model = orderBLL.GetAll();
            return View(model.ToPagedList(PageNumber, PageSize));
        }
        public JsonResult UpdateStatus(int id, int status)
        {
            var flag = orderBLL.UpdateOrder(id, status);
            return Json(flag);
        }
        public JsonResult DeleteOrder(int id)
        {
            var dsCt = orderBLL.GetOrderDetailByID(id);
            foreach(var item in dsCt)
            {
                var f = productBLL.UpdateQuantity1(item.ProductID, item.Quantity);
                if (!f)
                    break;
            }
            var flag = orderBLL.DeleteOrder(id);
            
            return Json(flag);
        }
        public JsonResult ViewDetail(int idOrder)
        {
            var order = orderBLL.GetOrderByID(idOrder);
            var dsCt = orderBLL.GetOrderDetailByID(idOrder);
            var orderVM = new OrderViewModel();
            orderVM.OrderName = order.OrderName;
            orderVM.OrderAddress = order.OrderAddress;
            orderVM.OrderEmail = order.OrderEmail;
            orderVM.OrderPhone = order.OrderPhone;
            orderVM.OrderNotes = order.OrderNotes;
            orderVM.CreatedDate = order.CreatedDate.ToString();
            orderVM.TotalMoney = string.Format("{0:#,##0}", order.TotalMoney) + " VNĐ";
            orderVM.lstOrderDetail = new List<OrderDetailViewModel>();
            foreach (var item in dsCt)
            {
                var x = new OrderDetailViewModel();
                x.Name = item.Product.Name;
                x.Image = item.Product.Image;
                x.Price = string.Format("{0:#,##0}", item.Price) + " VNĐ";
                x.Quantity = item.Quantity;
                x.TotalMoney = string.Format("{0:#,##0}", item.Price * item.Quantity) + " VNĐ";
                orderVM.lstOrderDetail.Add(x);
            }
            return Json(orderVM, JsonRequestBehavior.AllowGet);
        }
    }
}