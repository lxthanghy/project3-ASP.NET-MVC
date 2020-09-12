using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedbackDAL
    {
        private DaxoneDbContext db = null;
        public FeedbackDAL()
        {
            db = new DaxoneDbContext();
        }
        public bool AddFeedback(Feedback fb)
        {
            try
            {
                db.Feedbacks.Add(fb);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
