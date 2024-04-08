namespace LT.model
{
    public class AppSettings
    {
        private ConnectionStrings _connectionStrings { get; set; }
        public AppSettings(ConnectionStrings connectionStrings) {
            _connectionStrings = connectionStrings;
        }

        public string? GetLTContextConnectionString()
        {
            return _connectionStrings.GetLTContext();
        }
        public string? GetIdentityContextConnectionString()
        {
            return _connectionStrings.GetIdentityDbContext();
        }

    }
}