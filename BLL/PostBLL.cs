using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using Utility;

namespace BLL
{
    public class PostBLL
    {
        private PostDAL postDAL;
        public PostBLL()
        {
            postDAL = new PostDAL();
        }
        public List<Post> GetAll()
        {
            return postDAL.GetAll();
        }
        public bool ThemTinTuc(Post p)
        {
            if (string.IsNullOrEmpty(p.CreatedDate.ToString()))
            {
                p.CreatedDate = DateTime.Now;
            }
            p.Alias = Tools.GetAlias(p.Name);
            return postDAL.ThemTinTuc(p);
        }
        public bool SuaTinTuc(Post p)
        {
            if (string.IsNullOrEmpty(p.ModifiedDate.ToString()))
            {
                p.CreatedDate = DateTime.Now;
            }
            p.Alias = Tools.GetAlias(p.Name);
            return postDAL.SuaTinTuc(p);
        }
        public bool XoaTinTuc(int id)
        {
            return postDAL.XoaTinTuc(id);
        }
        public Post GetByID(int id)
        {
            return postDAL.GetByID(id);
        }
    }
}
