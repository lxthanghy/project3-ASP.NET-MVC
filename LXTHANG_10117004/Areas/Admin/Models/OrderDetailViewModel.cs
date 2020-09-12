using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class OrderDetailViewModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string TotalMoney { get; set; }
    }
}