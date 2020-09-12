using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using Utility;

namespace BLL
{
    public class ReviewBLL
    {
        private ReviewDAL reviewDAL;
        public ReviewBLL()
        {
            reviewDAL = new ReviewDAL();
        }
        //Load đánh giá
        public List<Review> GetReviews(int idProduct)
        {
            return reviewDAL.GetReviews(idProduct);
        }
        public int AddReview(int userID, int productID, string txtReview)
        {
            Review rv = new Review();
            rv.UserID = userID;
            rv.ProductID = productID;
            rv.Content = txtReview;
            rv.CreatedDate = DateTime.Now;
            return reviewDAL.AddReview(rv);
        }
    }
}
