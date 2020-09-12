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
    public class MenuBLL
    {
        private MenuDAL menuDAL = null;
        public MenuBLL()
        {
            menuDAL = new MenuDAL();
        }
        public List<Menu> GetAll()
        {
            return menuDAL.GetAll();
        }
    }
}
