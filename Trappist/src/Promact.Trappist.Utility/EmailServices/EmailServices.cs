using System.Threading.Tasks;
using MimeKit;
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
                http://localhost:50805/Account/ResetPassword?userId=712e6d24-b1a4-4183-8115-cacf1286f9bc&code=CfDJ8LI6KkQzxG1BuZLRHzRNFTTfELj2MJrolOzQNDgV8OGb8Wvdgy9ef%2BgQc3icTVo%2B5sO0TElLVGCG57vC9C0aw0R72kvUXr1CPbLkC9lOGLa%2BzGKk3Rc8UKW7KsLDeb9S6wTugWf7jLzofmiQRO%2BcDoBJV%2BjNjc2AhupTFuYYOZzy%2FgHXjXdLu629awz4HuSBTWSZ38CZi4wV7q%2Fi2OFJC3FK1Nql8yfi4sW67Fcsw7KoAJLvoU1xvKhG5NQ%2BXw3nyg%3D%3Dawait client.AuthenticateAsync("suparna@promactinfo.com", "6UBracHAju");
                client.Send(emailMessage);
                    client.Disconnect(true);
                }
                return true;
        }
        #endregion
    }
}

