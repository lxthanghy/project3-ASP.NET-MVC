using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class PostDAL
    {
        private DaxoneDbContext db = null;
        public PostDAL()
        {
            db = new DaxoneDbContext();
        }
        public List<Post> GetAll()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate).ToList();
        }
        public bool ThemTinTuc(Post p)
        {
            try
            {
                db.Posts.Add(p);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SuaTinTuc(Post p)
        {
            try
            {
                var post = db.Posts.Find(p.ID);
                post.Name = p.Name;
                post.CategoryID = p.CategoryID;
                post.Content = p.Content;
                post.ModifiedDate = p.ModifiedDate;
                post.ModifiedBy = p.ModifiedBy;
                post.Status = p.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool XoaTinTuc(int id)
        {
            try
            {
                var post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Post GetByID(int id)
        {
            return db.Posts.Find(id);
        }
    }
}
