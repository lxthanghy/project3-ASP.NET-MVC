using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class ProductCategoryBLL
    {
        private ProductCategoryDAL productCategoryDAL = null;
        public ProductCategoryBLL()
        {
            productCategoryDAL = new ProductCategoryDAL();
        }
        public List<ProductCategory> GetAll()
        {
            return productCategoryDAL.GetAll();
        }
        public string GetName(int id)
        {
            return productCategoryDAL.GetName(id);
        }
        public List<ProductCategoryLoadOption> LoadOption()
        {
            return productCategoryDAL.LoadOption();
        }
        public List<ProductCategory> GetByParentID(int parentID)
        {
            return productCategoryDAL.GetByParentID(parentID);
        }
    }
}
