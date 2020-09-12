using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HomeProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int? Promotion { get; set; }
        public string Alias { get; set; }
        public string CategoryAlias { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
    }
}
