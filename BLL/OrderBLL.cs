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
    public class OrderBLL
    {
        private OrderDAL orderDAL;
        private OrderDetailDAL orderDetailDAL;
        public OrderBLL()
        {
            orderDAL = new OrderDAL();
            orderDetailDAL = new OrderDetailDAL();
        }
        public List<Order> GetAll()
        {
            return orderDAL.GetAll();
        }
        public int SoLuongDonhang()
        {
            return orderDAL.GetAll().Count;
        }
        public int SoLuongDonHangChuaXacThuc()
        {
            var data = orderDAL.GetAll().Count(x => x.PaymentStatus == 0);
            return data;
        }
        public int SoLuongDonHangDaThanhToan()
        {
            var data = orderDAL.GetAll().Count(x => x.PaymentStatus == 3);
            return data;
        }
        public int SoLuongDonHangDaHuy()
        {
            var data = orderDAL.GetAll().Count(x => x.PaymentStatus == 4);
            return data;
        }
        public Order GetOrderByID(int id)
        {
            return orderDAL.GetByID(id);
        }
        public List<OrderDetail> GetOrderDetailByID(int id)
        {
            return orderDetailDAL.GetByIdOrder(id);
        }
        public bool UpdateOrder(int id, int status)
        {
            return orderDAL.UpdateOrder(id, status);
        }
        public bool DeleteOrder(int id)
        {
            return orderDAL.DeleteOrder(id);
        }
    }
}
