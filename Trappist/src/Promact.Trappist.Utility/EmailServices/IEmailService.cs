namespace Promact.Trappist.Utility.EmailServices
{
    public interface IEmailService
    {
        bool SendMail(string userName,string password,string server,int port,string body,string to);
    }
}
