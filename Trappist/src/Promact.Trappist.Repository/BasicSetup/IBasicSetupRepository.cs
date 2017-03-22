using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using System.Threading.Tasks;

namespace Promact.Trappist.Repository.BasicSetup
{
    public interface IBasicSetupRepository
    {
        /// <summary>
        /// This method used for creating the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if a user created then return true else return false</returns>
        Task<ServiceResponse> RegisterUser(BasicSetupModel model);

        /// <summary>
        /// This method used for validating connection string
        /// </summary>
        /// <param name="model"></param>
        /// <returns>If valid then return true else return false</returns>
        ServiceResponse ValidateConnectionString(BasicSetupModel model);

        /// <summary>
        /// This method used for verifying Email settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if valid email settings then return true else false</returns>
        bool ValidateEmailSetting(BasicSetupModel model);

        /// <summary>
        /// This method used for checking SetupConfig.json file exist or not
        /// </summary>
        /// <returns>If exist then return true or return false</returns>
        bool FileExist();
    }
}
