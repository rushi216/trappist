using Promact.Trappist.DomainModel.ApplicationClasses;
using System.Threading.Tasks;

namespace Promact.Trappist.Repository.BasicSetup
{
    public interface IBasicSetupRepository
    {
        Task<bool> RegisterUser(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model);
        Task<bool> ValidateConnectionString(ConnectionStringParamters model);
        Task<EmailResponse> ValidateEmailSetting(EmailSettings model);
        Task<bool> SaveSetupParameter();
    }
}
