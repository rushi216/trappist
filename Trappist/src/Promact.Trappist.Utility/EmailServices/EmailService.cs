using System;
using System.Threading.Tasks;
using Promact.Trappist.DomainModel.ApplicationClasses;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Promact.Trappist.Utility.EmailServices
{
    public class EmailService : IEmailService
    {
        #region IEmailService Method
        //Method used for sending mail
        public async Task<EmailResponse> SendMail(EmailSettings email)
        {
            var response = new EmailResponse();
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(email.UserName));
                emailMessage.To.Add(new MailboxAddress("hardik.patel@promactinfo.com"));
                using(var client=new SmtpClient())
                {
                    client.Connect(email.Server, email.Port, SecureSocketOptions.None);
                    await client.AuthenticateAsync(email.UserName, email.Password);
                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
                response.IsMailSent = true;
                return response;
            }
            catch(Exception ex)
            {
                response.IsMailSent = false;
                response.ErrorMessage = ex.ToString();
                return response;
            }
        }
        #endregion
    }
}
