using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.Marshalling;

namespace LT.model
{
    public class RabbitMQSettings
    {
        protected readonly string? _HostName;
        protected readonly string? _Password;
        protected readonly string? _UserName;
        protected readonly string? _ScoreQueueName;
        public RabbitMQSettings(IConfiguration configuration)
        {
            _HostName = configuration["RabbitMQSettings:HostName"];
            _Password = configuration["RabbitMQSettings:Password"];
            _UserName = configuration["RabbitMQSettings:UserName"];
            _ScoreQueueName = configuration["RabbitMQSettings:ScoreQueueName"];
        }

        public string? GetHostName()
        {
            return _HostName;
        }
        public string? GetPassword()
        {
            return _Password;
        }
        public string? GetUserName()
        {
            return _UserName;
        }
        public string? GetScoreQueueName()
        {
            return _ScoreQueueName;
        }

    }
}