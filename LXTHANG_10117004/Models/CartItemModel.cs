using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Models
{
    [Serializable]
    public class CartItemModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string strPrice { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}