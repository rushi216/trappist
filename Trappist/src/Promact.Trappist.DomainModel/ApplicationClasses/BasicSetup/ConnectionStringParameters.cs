namespace Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup
{
    public class ConnectionStringParameters
    {
        public string ConnectionString { get; set; }
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string IsSqlAuthentication { get; set; }
    }
}