using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MoreImages { get; set; }
        public string Frame { get; set; }
        public string Rims { get; set; }
        public string Tires { get; set; }
        public string Weight { get; set; }
        public string Weightlimit { get; set; }
        public decimal Price { get; set; }
        public string StrPrice { get; set; }
        public int? Promotion { get; set; }
        public string StrPromotion { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public string StrCategory { get; set; }
        public int SupplierID { get; set; }
        public string StrSupplier { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
        public bool HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public int? ViewCount { get; set; }
    }
}