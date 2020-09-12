using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class SupplierDAL
    {
        private DaxoneDbContext db = null;
        public SupplierDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<Supplier> GetAll()
        {
            return db.Suppliers.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<SupplierLoadOption> LoadOption()
        {
            List<SupplierLoadOption> data = new List<SupplierLoadOption>();
            var x = from y in db.Suppliers
                    select new SupplierLoadOption
                    {
                        ID = y.ID,
                        Name = y.Name
                    };
            data = x.ToList();
            return data;
        }
        public string GetName(int id)
        {
            return db.Suppliers.Find(id).Name;
        }
        public bool AddSupplier(Supplier sl)
        {
            try
            {
                db.Suppliers.Add(sl);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Supplier GetById(int id)
        {
            try
            {
                return db.Suppliers.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool EditSupplier(Supplier sl)
        {
            try
            {
                var x = db.Suppliers.Find(sl.ID);
                x.Name = sl.Name;
                x.Address = sl.Address;
                x.Email = sl.Email;
                x.Mobile = sl.Mobile;
                x.CreatedBy = sl.CreatedBy;
                if (!string.IsNullOrEmpty(sl.CreatedDate.ToString()))
                    x.CreatedDate = sl.CreatedDate;
                else
                    x.CreatedDate = DateTime.Now;
                x.ModifiedBy = sl.ModifiedBy;
                if (!string.IsNullOrEmpty(sl.ModifiedDate.ToString()))
                    x.ModifiedDate = sl.ModifiedDate;
                else
                    x.ModifiedDate = DateTime.Now;
                x.Status = sl.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteSupplier(int id)
        {
            try
            {
                var sl = db.Suppliers.Find(id);
                db.Suppliers.Remove(sl);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
