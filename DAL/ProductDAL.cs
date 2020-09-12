using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ProductDAL
    {
        private DaxoneDbContext db = null;
        public ProductDAL()
        {
            db = new DaxoneDbContext();
        }
        //Lấy tất cả sản phẩm (all thuộc tính)
        public List<Product> GetAll()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToList();
        }
        //Lấy tất cả sản phẩm home product (Một số thuộc tính)
        public List<HomeProduct> GetAllHomeProduct()
        {
            var data = db.Products.Where(x => x.Status == true).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy sản phẩm theo ID
        public Product GetById(int id)
        {
            try
            {
                return db.Products.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Thêm sản phẩm
        public bool AddProduct(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Sửa sản phẩm
        public bool EditProduct(Product p)
        {
            try
            {
                var x = db.Products.Find(p.ID);
                x.Name = p.Name;
                x.Image = p.Image;
                x.MoreImages = p.MoreImages;
                x.Description = p.Description;
                x.Frame = p.Frame;
                x.Rims = p.Rims;
                x.Tires = p.Tires;
                x.Weight = p.Weight;
                x.Weightlimit = p.Weightlimit;
                x.Price = p.Price;
                x.Promotion = p.Promotion;
                x.Quantity = p.Quantity;
                x.CategoryID = p.CategoryID;
                x.SupplierID = p.SupplierID;
                x.CreatedBy = p.CreatedBy;
                x.CreatedDate = p.CreatedDate;
                x.ModifiedBy = p.ModifiedBy;
                x.ModifiedDate = p.ModifiedDate;
                x.Status = p.Status;
                x.HomeFlag = p.HomeFlag;
                x.HotFlag = p.HotFlag;
                x.ViewCount = 0;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Xóa sản phẩm
        public bool DeleteProduct(int id)
        {
            try
            {
                var x = db.Products.Find(id);
                db.Products.Remove(x);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Lấy sản phẩm hiển thị ra trang chủ
        public List<HomeProduct> SanPhamHome()
        {
            var data = db.Products.Where(x => x.Status == true && x.HomeFlag == true).OrderByDescending(x => x.CreatedDate).Take(8).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy sản phẩm mới nhất hiển thị ra trang chủ
        public List<HomeProduct> SanPhamMoiNhatHome()
        {
            var data = db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(4).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy các sản phẩm liên quan
        public List<HomeProduct> SanPhamLienQuan(int id, int SupplierID)
        {
            var data = db.Products.Where(x => x.Status == true && x.SupplierID == SupplierID && x.ID != id).Take(5).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy sản phẩm (AddToCart)
        public HomeProduct ProductCart(int id)
        {
            var product = db.Products.Where(x => x.ID == id).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
            }).FirstOrDefault();
            return product;
        }
        //Lấy tất cả sản phẩm theo loại sản phẩm
        public List<HomeProduct> LaySanPhamTheoLoai(int idLoai)
        {
            var data = db.Products.Where(x => x.Status == true && x.CategoryID == idLoai).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                CategoryAlias = x.ProductCategory.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy tất cả sản phẩm theo loại sản phẩm và nhà cung cấp
        public List<HomeProduct> LaySanPhamTheoLoaiVaNhaCC(int idCate, int idSupp)
        {
            var data = db.Products.Where(x => x.Status == true && x.CategoryID == idCate && x.SupplierID == idSupp).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                CategoryAlias = x.ProductCategory.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy tất cả sản phẩm đang sale
        public List<HomeProduct> LaySanPhamSale()
        {
            var data = db.Products.Where(x => x.Status == true && x.Promotion != null).Select(x => new HomeProduct
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.Image,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Promotion = x.Promotion,
                Alias = x.Alias,
                SupplierName = x.Supplier.Name,
                CategoryName = x.ProductCategory.Name
            }).ToList();
            return data;
        }
        //Lấy số lượng qua ID
        public int GetQuantity(int id)
        {
            return db.Products.Find(id).Quantity;
        }
        //Cập nhật số lượng khi đặt hàng
        public bool UpdateQuantity(int id, int quantity)
        {
            try
            {
                var product = db.Products.Find(id);
                product.Quantity -= quantity;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Cập nhật số lượng khi đơn hàng bị hủy
        public bool UpdateQuantity1(int id, int quantity)
        {
            try
            {
                var product = db.Products.Find(id);
                product.Quantity += quantity;
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
