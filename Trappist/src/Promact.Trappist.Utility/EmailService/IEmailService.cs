using System.Threading.Tasks;
namespace Promact.Trappist.Utility.EmailServices
{
    public interface IEmailService
    {
        #region "SendEmail"
        /// <summary>
        /// this method is used to send mail 
        /// </summary>
        /// <param name="toEmail">email id of the user</param>
        /// <param name="subject">subject of the email</param>
        /// <param name="message">message passed in mailbody</param>
        /// <returns></returns>
        Task<bool> SendEmailAsync(string toEmail,string subject,string message);
        #endregion
    }
}