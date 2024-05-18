namespace LT.model
{
    public class AppSettings
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly JwtSettings _jwtSettings;
        private readonly RabbitMQSettings _rabbitmqSettings;

        public AppSettings(ConnectionStrings connectionStrings, JwtSettings jwtSettings, RabbitMQSettings rabbitmqSettings)
        {
            _connectionStrings = connectionStrings;
            _jwtSettings = jwtSettings;
            _rabbitmqSettings = rabbitmqSettings;
        }

        #region JWTSettings
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
        #endregion

        #region RabbitMQSettings
        public string? GetHostName()
        {
            return _rabbitmqSettings.GetHostName();
        }
        public string? GetPassword()
        {
            return _rabbitmqSettings.GetPassword();
        }
        public string? GetUserName()
        {
            return _rabbitmqSettings.GetUserName();
        }
        public string? GetScoreQueueName()
        {
            return _rabbitmqSettings.GetScoreQueueName();
        }
        #endregion
    }
}