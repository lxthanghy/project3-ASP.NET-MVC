using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class PostCategoryDAL
    {
        private DaxoneDbContext db = null;
        public PostCategoryDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<PostCategory> GetAll()
        {
            return db.PostCategories.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public bool ThemLoaiTinTuc(PostCategory pc)
        {
            try
            {
                db.PostCategories.Add(pc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SuaLoaiTinTuc(PostCategory pc)
        {
            try
            {
                var p = db.PostCategories.Find(pc.ID);
                p.Name = pc.Name;
                p.Description = pc.Description;
                p.ModifiedDate = pc.ModifiedDate;
                p.ModifiedBy = pc.ModifiedBy;
                p.Status = pc.Status;
                p.HomeFlag = pc.HomeFlag;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool XoaLoaiTinTuc(int id)
        {
            try
            {
                var pc = db.PostCategories.Find(id);
                db.PostCategories.Remove(pc);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public PostCategory GetByID(int id)
        {
            return db.PostCategories.Find(id);
        }
        public List<PostCategoryLoadOption> LoadOption()
        {
            var data = db.PostCategories.Where(x => x.Status == true).Select(x => new PostCategoryLoadOption
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
            return data;
        }
    }
}
