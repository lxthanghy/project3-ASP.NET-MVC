using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ProductCategoryDAL
    {
        private DaxoneDbContext db = null;
        public ProductCategoryDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<ProductCategory> GetAll()
        {
            return db.ProductCategories.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public string GetName(int id)
        {
            return db.ProductCategories.Find(id).Name;
        }
        public List<ProductCategoryLoadOption> LoadOption()
        {
            List<ProductCategoryLoadOption> data = new List<ProductCategoryLoadOption>();
            var x = from y in db.ProductCategories
                    select new ProductCategoryLoadOption
                    {
                        ID = y.ID,
                        Name = y.Name
                    };
            data = x.ToList();
            return data;
        }
        public List<ProductCategory> GetByParentID(int parentID)
        {
            return db.ProductCategories
                .Where(x => x.ParentID == parentID && x.Status == true)
                .OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
