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
    public class RabbitMQMessageSender<T> : IRabbitMQMessageSender<T>
        where T : EntityBaseDto
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
            _connection = factory.CreateConnectionAsync().Result;
        }

        public async Task SendMessageAsync(T message, string queueName, CancellationToken c)
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);

            var props = new BasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = DeliveryModes.Persistent;

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            await channel.BasicPublishAsync(
                exchange: "", 
                routingKey: queueName,
                mandatory: true,
                basicProperties: props, 
                body: body,
                cancellationToken: c
            );
        }
    }
}
