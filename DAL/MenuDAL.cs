using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MenuDAL
    {
        private DaxoneDbContext db = null;
        public MenuDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<Menu> GetAll()
        {
            return db.Menus
                .Where(x => x.Status == true)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
        }
    }
}
