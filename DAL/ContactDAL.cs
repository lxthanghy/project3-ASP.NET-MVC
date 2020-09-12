using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ContactDAL
    {
        private DaxoneDbContext db = null;
        public ContactDAL()
        {
            db = new DaxoneDbContext();
        }
        public Contact GetContact()
        {
            return db.Contacts.Where(x => x.Status == true).First();
        }
    }
}
