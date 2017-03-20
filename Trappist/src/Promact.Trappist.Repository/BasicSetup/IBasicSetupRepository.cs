using Promact.Trappist.DomainModel.ApplicationClasses;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using System.Threading.Tasks;

namespace Promact.Trappist.Repository.BasicSetup
{
    public interface IBasicSetupRepository
    {
        Task<ServiceResponse> RegisterUser(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model);
        ServiceResponse ValidateConnectionString(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model);
        bool ValidateEmailSetting(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model);
        bool FileExist();
    }
}
