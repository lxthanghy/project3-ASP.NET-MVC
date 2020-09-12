using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class OrderDetailDAL
    {
        private DaxoneDbContext db = null;
        public OrderDetailDAL()
        {
            db = new DaxoneDbContext();
        }
        public bool AddOrderDetail(OrderDetail odt)
        {
            try
            {
                db.OrderDetails.Add(odt);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<OrderDetail> GetByIdOrder(int idOrder)
        {
            return db.OrderDetails.Where(x => x.OrderID == idOrder).ToList();
        }
    }
}
