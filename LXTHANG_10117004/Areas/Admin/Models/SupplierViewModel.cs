using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class SupplierViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}