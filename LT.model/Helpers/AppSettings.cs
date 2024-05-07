namespace LT.model
{
    public class AppSettings
    {
        private ConnectionStrings _connectionStrings { get; set; }
        private JwtSettings _jwtSettings { get; set; }
        public AppSettings(ConnectionStrings connectionStrings, JwtSettings jwtSettings)
        {
            _connectionStrings = connectionStrings;
            _jwtSettings = jwtSettings;
        }
        public string? GetIssuer()
        {
            return _jwtSettings.GetIssuer();
        }
        public string? GetAudience()
        {
            return _jwtSettings.GetAudience();
        }
        public string? GetKey()
        {
            return _jwtSettings.GetKey();
        }
    }
}