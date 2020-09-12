using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class SupplierBLL
    {
        private SupplierDAL supplierDAL = null;
        public SupplierBLL()
        {
            supplierDAL = new SupplierDAL();
        }
        public List<Supplier> GetAll()
        {
            return supplierDAL.GetAll();
        }
        public List<SupplierLoadOption> LoadOption()
        {
            return supplierDAL.LoadOption();
        }
        public string GetName(int id)
        {
            return supplierDAL.GetName(id);
        }
        public bool AddSupplier(Supplier sl)
        {
            if (!string.IsNullOrEmpty(sl.CreatedDate.ToString()))
                sl.CreatedDate = DateTime.ParseExact(sl.CreatedDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                sl.CreatedDate = DateTime.Now;
            return supplierDAL.AddSupplier(sl);
        }
        public Supplier GetById(int id)
        {
            return supplierDAL.GetById(id);
        }
        public bool EditSupplier(Supplier sl)
        {
            return supplierDAL.EditSupplier(sl);
        }
        public bool DeleteSupplier(int id)
        {
            return supplierDAL.DeleteSupplier(id);
        }
    }
}
