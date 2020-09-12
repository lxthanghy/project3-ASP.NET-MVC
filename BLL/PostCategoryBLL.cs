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
    public class PostCategoryBLL
    {
        private PostCategoryDAL postCategoryDAL;
        public PostCategoryBLL()
        {
            postCategoryDAL = new PostCategoryDAL();
        }
        public List<PostCategory> GetAll()
        {
            return postCategoryDAL.GetAll();
        }
        public bool ThemLoaiTinTuc(PostCategory pc)
        {
            if (string.IsNullOrEmpty(pc.CreatedDate.ToString()))
            {
                pc.CreatedDate = DateTime.Now;
            }
            pc.Alias = Tools.GetAlias(pc.Name);
            pc.ParentID = 3;
            return postCategoryDAL.ThemLoaiTinTuc(pc);
        }
        public bool SuaLoaiTinTuc(PostCategory pc)
        {
            if (string.IsNullOrEmpty(pc.ModifiedDate.ToString()))
            {
                pc.ModifiedDate = DateTime.Now;
            }
            pc.Alias = Tools.GetAlias(pc.Name);
            return postCategoryDAL.SuaLoaiTinTuc(pc);
        }
        public bool XoaLoaiTinTuc(int id)
        {
            return postCategoryDAL.XoaLoaiTinTuc(id);
        }
        public PostCategory GetByID(int id)
        {
            return postCategoryDAL.GetByID(id);
        }
        public List<PostCategoryLoadOption> LoadOption()
        {
            return postCategoryDAL.LoadOption();
        }
    }
}
