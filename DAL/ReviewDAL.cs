using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ReviewDAL
    {
        private DaxoneDbContext db = null;
        public ReviewDAL()
        {
            db = new DaxoneDbContext();
        }
        //Load đánh giá
        public List<Review> GetReviews(int idProduct)
        {
            return db.Reviews.Where(x => x.ProductID == idProduct).OrderByDescending(x => x.CreatedDate).ToList();
        }
        //Thêm đánh giá
        public int AddReview(Review rv)
        {
            try
            {
                db.Reviews.Add(rv);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
