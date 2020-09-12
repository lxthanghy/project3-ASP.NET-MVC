using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class AboutUsBLL
    {
        private AboutUsDAL aboutUsDAL;
        public AboutUsBLL()
        {
            aboutUsDAL = new AboutUsDAL();
        }
    }
}
