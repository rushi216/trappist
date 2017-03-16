using Promact.Trappist.DomainModel.ApplicationClasses;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using System.Threading.Tasks;

namespace Promact.Trappist.Repository.BasicSetup
{
    public interface IBasicSetupRepository
    {
        Task<ServiceResponse> RegisterUser(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model);
        Task<ServiceResponse> ValidateConnectionString(ConnectionStringParamters model);
        Task<EmailResponse> ValidateEmailSetting(EmailSettings model);
        Task<bool> SaveSetupParameter();
    }
}
