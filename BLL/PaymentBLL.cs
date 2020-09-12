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
    public class PaymentBLL
    {
        private OrderDAL orderDAL;
        private OrderDetailDAL orderDetailDAL;
        public PaymentBLL()
        {
            orderDAL = new OrderDAL();
            orderDetailDAL = new OrderDetailDAL();
        }
        public bool AddOrder(int idUser, string firstName, string lastName, string address, string email, string phone, string message, decimal totalMoney, out int idOrder)
        {
            Order od = new Order();
            od.UserID = idUser;
            od.OrderName = string.Format("{0} {1}", firstName, lastName);
            od.OrderAddress = address;
            od.OrderEmail = email;
            od.OrderPhone = phone;
            od.OrderNotes = message;
            od.CreatedDate = DateTime.Now;
            od.TotalMoney = totalMoney;
            od.PaymentStatus = 0;
            return orderDAL.AddOrder(od, out idOrder);
        }
        public bool AddOrderDetail(int idOrder, int idProduct, int quantity, decimal price)
        {
            OrderDetail odt = new OrderDetail();
            odt.OrderID = idOrder;
            odt.ProductID = idProduct;
            odt.Quantity = quantity;
            odt.Price = price;
            return orderDetailDAL.AddOrderDetail(odt);
        }
    }
}
