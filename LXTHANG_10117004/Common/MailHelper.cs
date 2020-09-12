using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace LXTHANG_10117004.Common
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"].ToString());
                var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var password = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var fromName = ConfigurationManager.AppSettings["FromName"].ToString();
                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };
                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                smtpClient.Send(mail);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }
    }
}