using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        private DaxoneDbContext db = null;
        public UserDAL()
        {
            db = new DaxoneDbContext();
        }
        //Lấy user qua username
        public User GetUser(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName.Equals(userName));
        }
        //Lấy số lượng user qua username
        public int GetCountUserName(string userName)
        {
            return db.Users.Count(x => x.UserName.Equals(userName));
        }
        //Lấy số lượng user qua email
        public int GetCountEmail(string email)
        {
            return db.Users.Count(x => x.Email.Equals(email));
        }
        //Thêm user
        public bool AddUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Check user có phải admin không
        public bool CheckAdmin(string userName)
        {
            var user = db.Users.Single(x => x.UserName.Equals(userName));
            if (user.isAdmin == true)
                return true;
            else
                return false;
        }
    }
}
