using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Promact.Trappist.Utility.EmailServices
{
    public class EmailService : IEmailService
    {
        #region IEmailService Method
        //Method used for sending mail to client
        public bool SendMail(string userName, string password, string server, int port, string body, string to)
        {          
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(userName));
                emailMessage.To.Add(new MailboxAddress(to));
                using (var client = new SmtpClient())
                {
                    client.Connect(server, port, SecureSocketOptions.None);
                    client.AuthenticateAsync(userName, password);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception)
            {                              
                return false;               
            }
        }
        #endregion
    }
}
