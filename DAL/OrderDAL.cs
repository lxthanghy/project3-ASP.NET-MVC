using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class OrderDAL
    {
        private DaxoneDbContext db = null;
        public OrderDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<Order> GetAll()
        {
            return db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public Order GetByID(int id)
        {
            return db.Orders.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool AddOrder(Order od, out int idOrder)
        {
            try
            {
                db.Orders.Add(od);
                db.SaveChanges();
                idOrder = od.ID;
                return true;
            }
            catch (Exception)
            {
                idOrder = 0;
                return false;
            }
        }
        public bool UpdateOrder(int id, int status)
        {
            try
            {
                var order = db.Orders.Find(id);
                order.PaymentStatus = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteOrder(int id)
        {
            try
            {
                var order = db.Orders.Find(id);
                db.Orders.Remove(order);
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
