using Azure.Messaging.ServiceBus;
using LT.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.messageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string? _connectionString;

        public AzureServiceBusMessageBus(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
        }

        public async Task PublishMessage(EntityBase message, string topicName)
        {
            if(_connectionString != null)
            {
                await using var client = new ServiceBusClient(_connectionString);
                ServiceBusSender sender = client.CreateSender(topicName);

                var jsonMessage = JsonConvert.SerializeObject(message);
                ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString()
                };

                await sender.SendMessageAsync(finalMessage);
                await client.DisposeAsync();
            }
        }
    }
}
