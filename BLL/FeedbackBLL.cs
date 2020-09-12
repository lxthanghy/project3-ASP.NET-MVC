using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using Utility;

namespace BLL
{
    public class FeedbackBLL
    {
        private FeedbackDAL feedbackDAL;
        public FeedbackBLL()
        {
            feedbackDAL = new FeedbackDAL();
        }
        public bool AddFeedback(string name, string email, string address, string phone, string message)
        {
            try
            {
                Feedback fb = new Feedback();
                fb.Name = name;
                fb.Email = email;
                fb.Address = address;
                fb.Phone = phone;
                fb.Content = message;
                fb.CreatedDate = DateTime.Now;
                fb.Status = true;
                var flag = feedbackDAL.AddFeedback(fb);
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
