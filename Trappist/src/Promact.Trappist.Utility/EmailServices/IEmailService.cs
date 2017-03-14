using Promact.Trappist.DomainModel.ApplicationClasses;
using System.Threading.Tasks;

namespace Promact.Trappist.Utility.EmailServices
{
    public interface IEmailService
    {
        Task<EmailResponse> SendMail(EmailSettings email);
    }
}
