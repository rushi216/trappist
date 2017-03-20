using System.Threading.Tasks;
namespace Promact.Trappist.Utility.EmailServices
{
    public interface IEmailServices
    {
        #region "SendEmail"
        /// <summary>
        /// this method is used to send mail to a perticular mailid with mailsubject and mailbody cnotent
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendEmailAsync(string email,string subject,string message);
        #endregion
    }
}