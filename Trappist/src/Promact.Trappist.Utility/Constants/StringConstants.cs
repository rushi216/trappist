namespace Promact.Trappist.Utility.Constants
{
    public class StringConstants : IStringConstants
    {       
        public string InvalidTestName
        {
            get
            {
                return "Invalid Test Name "; 
            }
        }
       
        public string Success
        {
            get
            {
                return "Test Created successfuly"; 
            }
        }

        #region Setup Constants
        /// <summary>
        /// property ConfigFolderName is called whenever required config folder name
        /// </summary>
        public string ConfigFolderName
        {
            get
            {
                return "Config";
            }
        }

        /// <summary>
        /// property SetupConfigFilename is called whenever required SetupConfig file name
        /// </summary>
        public string SetupConfigFileName
        {
            get
            {
                return "SetupConfig.json";
            }
        }
        #endregion

        #region "Account Constants"

        public string InavalidLoginError
        {
            get
            {
                return "Username or Password Is Invalid!";
            }
        }

        public string InavalidModelError
        {
            get
            {
                return "Invalid Login Attempt!";
            }
        }
        #endregion
    }
}
