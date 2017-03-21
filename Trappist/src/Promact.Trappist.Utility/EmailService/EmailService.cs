using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Promact.Trappist.Utility.Constants;
using Promact.Trappist.DomainModel.ApplicationClasses;

namespace Promact.Trappist.Utility.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        #region "Send Email"
        /// <summary>
        /// this method is used to send mail 
        /// </summary>
        /// <param name="toEmail">email address of receiver</param>
        /// <param name="subject">subject of the mail</param>
        /// <param name="message">content passed in mail body</param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.UserName));
            emailMessage.To.Add(new MailboxAddress(toEmail));
            emailMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.None);
                await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
            return true;
        }
        #endregion
    }
}

