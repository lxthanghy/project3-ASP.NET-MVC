using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LXTHANG_10117004.Areas.Admin.Models
{
    public class PostCategoryViewModel
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
        public bool HomeFlag { get; set; }
    }
}