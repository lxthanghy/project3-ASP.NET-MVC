using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LXTHANG_10117004.Common;
using LXTHANG_10117004.Models;
using BLL;
using DTO;

namespace LXTHANG_10117004.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewBLL reviewBLL = new ReviewBLL();
        public JsonResult GetReviews(int idProduct)
        {
            var data = reviewBLL.GetReviews(idProduct);
            var lstReview = new List<ReviewModel>();
            ReviewModel rv = null;
            data.ForEach(x =>
            {
                rv = new ReviewModel();
                rv.Name = x.User.Name;
                rv.Content = x.Content;
                rv.CreatedDate = x.CreatedDate.ToString();
                lstReview.Add(rv);
            });
            return Json(new
            {
                reviews = lstReview,
                count = lstReview.Count
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddReview(int idProduct, string txtReview)
        {
            if (string.IsNullOrWhiteSpace(txtReview))
            {
                return Json(-1);
            }
            else
            {
                if (Session[CommonConstants.USER_SESSION] != null)
                {
                    var user = (UserLoginModel)Session[CommonConstants.USER_SESSION];
                    var result = reviewBLL.AddReview(user.ID, idProduct, txtReview);
                    return Json(result);
                }
                else
                    return Json(2);
            }
        }
    }
}