using LXTHANG_10117004.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LXTHANG_10117004.Models;
using BLL;
using DTO;
using System.Web.Script.Serialization;

namespace LXTHANG_10117004.Controllers
{
    public class CartController : Controller
    {
        private ProductBLL productBLL = new ProductBLL();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        //Load giỏ hàng
        public JsonResult LoadCart()
        {
            var cartItems = new List<CartItemModel>();
            var totalMoney = (decimal)0;
            var countCart = 0;
            if (Session[CommonConstants.CART_SESSION] != null)
            {
                cartItems = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
                totalMoney = cartItems.Sum(x => x.Price * x.Quantity);
                countCart = cartItems.Count;
            }
            var strMoney = string.Format("{0:#,##0}", totalMoney);
            return Json(new
            {
                data = cartItems,
                money = strMoney,
                count = countCart
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddToCart(int id, int quantity)
        {
            try
            {
                var product = productBLL.ProductCart(id);
                var cartItems = new List<CartItemModel>();
                if (Session[CommonConstants.CART_SESSION] != null)
                {
                    cartItems = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
                    if (cartItems.Any(x => x.ID == id))
                    {
                        foreach (var item in cartItems)
                        {
                            if (item.ID == id)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        var cartItem = new CartItemModel();
                        cartItem.ID = product.ID;
                        cartItem.Name = product.Name;
                        if (product.Promotion != null)
                        {
                            cartItem.strPrice = string.Format("{0:#,##0}", product.NewPrice);
                            cartItem.Price = product.NewPrice;
                        }
                        else
                        {
                            cartItem.strPrice = string.Format("{0:#,##0}", product.OldPrice);
                            cartItem.Price = product.OldPrice;
                        }
                        cartItem.Image = product.Image;
                        cartItem.Quantity = quantity;
                        cartItems.Add(cartItem);
                        Session[CommonConstants.CART_SESSION] = cartItems;
                    }
                }
                else
                {
                    var cartItem = new CartItemModel();
                    cartItem.ID = product.ID;
                    cartItem.Name = product.Name;
                    if (product.Promotion != null)
                    {
                        cartItem.strPrice = string.Format("{0:#,##0}", product.NewPrice);
                        cartItem.Price = product.NewPrice;
                    }
                    else
                    {
                        cartItem.strPrice = string.Format("{0:#,##0}", product.OldPrice);
                        cartItem.Price = product.OldPrice;
                    }
                    cartItem.Image = product.Image;
                    cartItem.Quantity = quantity;
                    cartItems.Add(cartItem);
                    Session[CommonConstants.CART_SESSION] = cartItems;
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        //Xóa sản phẩm trong giỏ hàng
        public JsonResult DeleteCart(int id)
        {
            try
            {
                var cartItems = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
                var cartDelete = cartItems.Single(x => x.ID == id);
                cartItems.Remove(cartDelete);
                Session[CommonConstants.CART_SESSION] = cartItems;
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        //Xem giỏ hàng
        public ActionResult ViewCart()
        {
            var data = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
            ViewBag.Money = data.Sum(x => x.Price * x.Quantity);
            return View(data);
        }
        //Cập nhật giỏ hàng
        public JsonResult UpdateCart(string cartList)
        {
            int Flag = 0;
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<CartItemModel> jsonCart = serializer.Deserialize<List<CartItemModel>>(cartList);
                List<CartItemModel> sessionCart = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
                foreach (var cart in jsonCart)
                {
                    var cartItem = sessionCart.SingleOrDefault(x => x.ID == cart.ID && x.Quantity != cart.Quantity);
                    if (cartItem != null)
                    {
                        Flag = 1;
                        cartItem.Quantity = cart.Quantity;
                    }
                }
                Session[CommonConstants.CART_SESSION] = sessionCart;
                return Json(Flag);
            }
            catch (Exception)
            {
                return Json(2);
            }
        }
        //Xóa toàn bộ sản phẩm trong giỏ hàng
        public JsonResult DeleteAll()
        {
            try
            {
                Session[CommonConstants.CART_SESSION] = new List<CartItemModel>();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        //Check số lượng đặt hàng (ViewCard)
        public JsonResult CheckQuantity(int id, int quantity)
        {
            var cartItems = (List<CartItemModel>)Session[CommonConstants.CART_SESSION];
            var sl = cartItems.Where(x => x.ID == id).First().Quantity;
            var flag = productBLL.CheckQuantity(id, quantity);
            return Json(new
            {
                status = flag,
                qt = sl
            });
        }
        //Check số lượng đặt hàng (ViewDetail)
        public JsonResult CheckQuantity1(int id, int quantity)
        {
            var flag = productBLL.CheckQuantity(id, quantity);
            return Json(flag);
        }
    }
}