using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AboutUsDAL
    {
        private DaxoneDbContext db = null;
        public AboutUsDAL()
        {
            db = new DaxoneDbContext();
        }
    }
}
