using System;
using System.Threading.Tasks;
using MimeKit;
using System.Net.Http;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Promact.Trappist.Utility.EmailServices
{
    public class EmailServices: IEmailServices
    {
        #region "Send Email"
        /// <summary>
        /// this method is used to send mail to the user with a link 
        /// </summary>
        /// <param name="email">email address of the sender</param>
        /// <param name="subject">subject of the mail</param>
        /// <param name="message">content passed in mail body</param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {

                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(email));
                emailMessage.To.Add(new MailboxAddress("suparna@promactinfo.com"));
                emailMessage.Body = new TextPart("plain")
                {
                    Text = message
                };
           
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("webmail.promactinfo.com", 587, SecureSocketOptions.None);
                    await client.AuthenticateAsync("suparna@promactinfo.com", "6UBracHAju");
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                return true;
        }
        #endregion
    }
}

