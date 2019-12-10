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
        public static void SendEmail(string toAddress,string UserName,string message)
        { 
            MailAddress _fromMailAddress = new MailAddress("web.market.api.project@gmail.com");
            string password = "59246a01b58aadced8f913a1350af1ca65465b378b26b9f6ed388845695b3bdf";
            MailAddress _toAddress = new MailAddress(toAddress, UserName);

            using(MailMessage mailMessage = new MailMessage(_fromMailAddress, _toAddress))
            using(SmtpClient smtpClient = new SmtpClient())
            {
                mailMessage.Subject = "Заказ из интернет магазина";
                mailMessage.Body = message;

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
