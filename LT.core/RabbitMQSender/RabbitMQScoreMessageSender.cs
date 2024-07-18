using LT.model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender(IConfiguration configuration)
        {
            _hostname = configuration.GetSection("RabbitMQSettings:HostName").Value!;
            _password = configuration.GetSection("RabbitMQSettings:Password").Value!;
            _userName = configuration.GetSection("RabbitMQSettings:UserName").Value!;
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                Password = _password,
                UserName = _userName,
            };
            _connection = factory.CreateConnection();
        }

        public void SendMessage(EntityBaseDto message, string? queueName)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange:"", routingKey:queueName, basicProperties:null, body:body);
        }
    }
}
