using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using DAL;
using DTO;
using Utility;

namespace BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = null;
        public ProductBLL()
        {
            productDAL = new ProductDAL();
        }
        public List<Product> GetAll()
        {
            return productDAL.GetAll();
        }
        public int SoLuongSanPham()
        {
            return productDAL.GetAll().Count();
        }
        public List<HomeProduct> GetAllHomeProduct()
        {
            var data = productDAL.GetAllHomeProduct();
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public Product GetById(int id)
        {
            return productDAL.GetById(id);
        }
        public bool AddProduct(Product p)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var lstImages = serializer.Deserialize<List<string>>(p.MoreImages);
            XElement element = new XElement("Images");
            foreach (var item in lstImages)
            {
                element.Add(new XElement("Image", item));
            }
            p.Alias = Tools.GetAlias(p.Name);
            p.MoreImages = element.ToString();
            if (!string.IsNullOrEmpty(p.CreatedDate.ToString()))
                p.CreatedDate = DateTime.ParseExact(p.CreatedDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                p.CreatedDate = DateTime.Now;
            p.ViewCount = 0;
            return productDAL.AddProduct(p);
        }
        public bool EditProduct(Product p)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var lstImages = serializer.Deserialize<List<string>>(p.MoreImages);
            XElement element = new XElement("Images");
            foreach (var item in lstImages)
            {
                element.Add(new XElement("Image", item));
            }
            p.Alias = Tools.GetAlias(p.Name);
            p.MoreImages = element.ToString();
            if (string.IsNullOrEmpty(p.CreatedDate.ToString()))
                p.CreatedDate = DateTime.Now;
            if (string.IsNullOrEmpty(p.ModifiedDate.ToString()))
                p.ModifiedDate = DateTime.Now;
            return productDAL.EditProduct(p);
        }
        public bool DeleteProduct(int id)
        {
            return productDAL.DeleteProduct(id);
        }
        //Lấy sản phẩm hiển thị ra trang chủ
        public List<HomeProduct> SanPhamHome()
        {
            var data = productDAL.SanPhamHome();
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public List<HomeProduct> SanPhamMoiNhatHome()
        {
            var data = productDAL.SanPhamMoiNhatHome();
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public List<HomeProduct> SanPhamLienQuan(int id, int SupplierID)
        {
            var data = productDAL.SanPhamLienQuan(id, SupplierID);
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public List<HomeProduct> LaySanPhamTheoLoai(int idLoai)
        {
            var data = productDAL.LaySanPhamTheoLoai(idLoai);
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public List<HomeProduct> LaySanPhamTheoLoaiVaNhaCC(int idCate, int idSupp)
        {
            var data = productDAL.LaySanPhamTheoLoaiVaNhaCC(idCate, idSupp);
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            return data;
        }
        public HomeProduct ProductCart(int id)
        {
            var product = productDAL.ProductCart(id);
            if (product.Promotion != null)
            {
                double promotion = (double)product.Promotion / 100;
                product.NewPrice = Convert.ToDecimal((double)product.OldPrice - (double)product.OldPrice * promotion);
            }
            return product;
        }
        //Tìm kiếm theo tên
        public List<HomeProduct> SearchByName(string name)
        {
            var data = productDAL.GetAllHomeProduct();
            data.ForEach(x =>
            {
                if (x.Promotion != null)
                {
                    double promotion = (double)x.Promotion / 100;
                    x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
                }
            });
            var result = new List<HomeProduct>();
            result = data.Where(x => x.Name.ToLower().Contains(name.ToLower()) || x.SupplierName.ToLower().Contains(name.ToLower())).ToList();
            return result;
        }
        //Lấy tất cả sản phẩm đang sale
        public List<HomeProduct> LaySanPhamSale()
        {
            var data = productDAL.LaySanPhamSale();
            data.ForEach(x =>
            {
                double promotion = (double)x.Promotion / 100;
                x.NewPrice = Convert.ToDecimal((double)x.OldPrice - (double)x.OldPrice * promotion);
            });
            return data;
        }
        //Check số lượng đặt hàng
        public bool CheckQuantity(int id, int quantity)
        {
            var quantityProduct = productDAL.GetQuantity(id);
            if (quantityProduct <= quantity)
                return true;
            else
                return false;
        }
        //Update số lượng khi đặt hàng
        public bool UpdateQuantity(int id, int quantity)
        {
            return productDAL.UpdateQuantity(id, quantity);
        }
        //Update số lượng khi đơn hàng bị hủy
        public bool UpdateQuantity1(int id, int quantity)
        {
            return productDAL.UpdateQuantity1(id, quantity);
        }
    }
}
