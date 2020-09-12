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
    public class ContactBLL
    {
        private ContactDAL contactDAL;
        public ContactBLL()
        {
            contactDAL = new ContactDAL();
        }
        public Contact GetContact()
        {
            return contactDAL.GetContact();
        }
    }
}
