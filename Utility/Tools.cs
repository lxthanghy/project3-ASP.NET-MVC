using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Utility
{
    public class Tools
    {
        public static string GetAlias(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            string result = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Trim().ToLower();
            List<string> tmp = result.Split(' ').ToList();
            List<string> newTmp = new List<string>();
            tmp.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x))
                {
                    if (x.Contains('.'))
                    {
                        List<string> dot = x.Split('.').ToList();
                        dot.ForEach(y => { newTmp.Add(y); });
                    }
                    else
                        newTmp.Add(x);
                }
            });
            string z = "";
            for (int i = 0; i < newTmp.Count; i++)
            {
                if (i != newTmp.Count - 1)
                    z += newTmp[i] + "-";
                else
                    z += newTmp[i];
            }
            return z;
        }
        public static List<string> LoadMoreImages(string xml)
        {
            XElement xImages = XElement.Parse(xml);
            List<string> src = new List<string>();
            foreach (XElement xElement in xImages.Elements())
            {
                src.Add(xElement.Value);
            }
            return src;
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public static string GetCode()
        {
            Random rd = new Random();
            return rd.Next(100000, 999999).ToString();
        }
    }
}
