using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Promact.Trappist.Utility.EmailServices
{
    public class EmailService : IEmailService
    {
        #region IEmailService Method
        /// <summary>
        /// This method used for sending mail to client
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="body"></param>
        /// <param name="to"></param>
        /// <returns>It return true if mail sent else return false.</returns>
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
