using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LXTHANG_10117004.Models;
using LXTHANG_10117004.Common;
using System.Xml.Linq;
using BLL;
using Utility;

namespace LXTHANG_10117004.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentBLL paymentBLL = new PaymentBLL();
        private ProductBLL productBLL = new ProductBLL();
        [HttpGet]
        // GET: Payment
        public ActionResult Payment()
        {
            var cards = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
            ViewBag.Money = cards.Sum(x => x.Price * x.Quantity);
            return View(cards);
        }
        [HttpPost]
        public JsonResult Payment(string firstName, string lastName, string address, string email, string phone, string message)
        {
            try
            {
                var cards = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
                var user = (UserLoginModel)Session[CommonConstants.USER_SESSION];
                var idUser = user == null ? 0 : user.ID;
                var totalMoney = cards.Sum(x => x.Price * x.Quantity);
                int idOrder;
                bool flag = paymentBLL.AddOrder(idUser, firstName, lastName, address, email, phone, message, totalMoney, out idOrder);
                foreach(var item in cards)
                {
                    paymentBLL.AddOrderDetail(idOrder, item.ID, item.Quantity, item.Price);
                    bool f = productBLL.UpdateQuantity(item.ID, item.Quantity);
                    if (!f)
                        break;
                }
                Session[CommonConstants.CART_SESSION] = null;
                return Json(flag);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath("~/Assets/Client/data/Provinces_Data.xml"));
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value.Equals("province"));
            var dsTinh = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                dsTinh.Add(province);
            }
            return Json(new
            {
                data = dsTinh,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadDistrict(int idProvince)
        {
            var xmlDoc = XDocument.Load(Server.MapPath("~/Assets/Client/data/Provinces_Data.xml"));
            var tinh = xmlDoc
                .Element("Root")
                .Elements("Item")
                .Single(x => x.Attribute("type").Value.Equals("province") &&
                int.Parse(x.Attribute("id").Value) == idProvince);
            var dsHuyen = new List<DistrictModel>();
            DistrictModel district = null;
            var dataHuyen = tinh.Elements("Item").Where(x => x.Attribute("type").Value.Equals("district"));
            foreach (var item in dataHuyen)
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                dsHuyen.Add(district);
            }
            return Json(new
            {
                data = dsHuyen,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadPrecinct(int idProvince, int idDistrict)
        {
            var xmlDoc = XDocument.Load(Server.MapPath("~/Assets/Client/data/Provinces_Data.xml"));
            var tinh = xmlDoc
                .Element("Root")
                .Elements("Item")
                .Single(x => x.Attribute("type").Value.Equals("province") &&
                int.Parse(x.Attribute("id").Value) == idProvince);
            var huyen = tinh
                .Elements("Item")
                .Single(x => x.Attribute("type").Value.Equals("district") &&
                int.Parse(x.Attribute("id").Value) == idDistrict);

            var dsXa = new List<PrecinctModel>();
            PrecinctModel precinct = null;
            var dataXa = huyen.Elements("Item").Where(x => x.Attribute("type").Value.Equals("precinct"));
            foreach (var item in dataXa)
            {
                precinct = new PrecinctModel();
                precinct.ID = int.Parse(item.Attribute("id").Value);
                precinct.Name = item.Attribute("value").Value;
                dsXa.Add(precinct);
            }
            return Json(new
            {
                data = dsXa,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendCode(string email)
        {
            bool Flag = false;
            string code = "";
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/template/vericode_template.html"));
                code = Tools.GetCode();
                content = content.Replace("{{code}}", code);
                MailHelper.SendMail(email, "Mã xác thực email", content);
                Flag = !Flag;
            }
            catch (Exception)
            {
            }
            return Json(new
            {
                code = code,
                status = Flag
            });
        }
    }
}