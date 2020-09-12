using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public string OrderName { get; set; }
        public string OrderAddress { get; set; }
        public string OrderEmail { get; set; }
        public string OrderPhone { get; set; }
        public string OrderNotes { get; set; }
        public string CreatedDate { get; set; }
        public string TotalMoney { get; set; }
        public List<OrderDetailViewModel> lstOrderDetail { get; set; }
    }
}