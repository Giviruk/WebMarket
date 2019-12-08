using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebMarket.Logic.Email
{
    public static class EmailSender
    {
        public static void SendEmail(string toAddress,string UserName)
        { 
            MailAddress _fromMailAddress = new MailAddress("web.market.api.project@gmail.com");
            string password = "Спрашивайте у Артура";
            MailAddress _toAddress = new MailAddress(toAddress, UserName);

            using(MailMessage mailMessage = new MailMessage(_fromMailAddress, _toAddress))
            using(SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.Subject = "Курсач";
                mailMessage.Body = "Слыш работать";

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_fromMailAddress.Address, password);

                smtpClient.Send(mailMessage);
            }
        }
    }
}
