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
    public class UserBLL
    {
        private UserDAL userDAL;
        public UserBLL()
        {
            userDAL = new UserDAL();
        }
        //Đăng nhập
        public int Login(string userName, string passWord)
        {
            var user = userDAL.GetUser(userName);
            if (user == null)
                return 0;
            if (userDAL.CheckAdmin(userName) == true)
                return 1;
            else if (user.Password == Tools.MD5Hash(passWord))
            {
                if (user.Status == false)
                    return 2;
                else
                    return 3;
            }
            return 4;
        }
        public int Login1(string userName, string passWord)
        {
            var user = userDAL.GetUser(userName);
            if (user == null)
                return 0;
            else if (user.Password == Tools.MD5Hash(passWord))
            {
                if (userDAL.CheckAdmin(userName) == false)
                    return 1;
                else
                    return 2;
            }
            return 3;
        }
        public bool Register(string userName, string passWord, string name, string address, string email, string mobile)
        {
            User user = new User();
            user.UserName = userName;
            user.Password = Tools.MD5Hash(passWord);
            user.Name = name;
            user.Address = address;
            user.Email = email;
            user.Mobile = mobile;
            user.CreatedDate = DateTime.Now;
            user.Status = true;
            user.isAdmin = false;
            bool result = userDAL.AddUser(user);
            return result;
        }
        //Kiểm tra tồn tại username
        public bool CheckUserName(string userName)
        {
            bool result = userDAL.GetCountUserName(userName) > 0 ? true : false;
            return result;
        }
        //Kiểm tra tồn tại email
        public bool CheckEmail(string email)
        {
            bool result = userDAL.GetCountEmail(email) > 0 ? true : false;
            return result;
        }
        //Lấy về user qua username
        public User GetUser(string userName)
        {
            return userDAL.GetUser(userName);
        }
    }
}
