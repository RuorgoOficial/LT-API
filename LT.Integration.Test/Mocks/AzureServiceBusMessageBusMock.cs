using AutoFixture;
using Azure.Messaging.ServiceBus;
using LT.dal.Abstractions;
using LT.messageBus;
using LT.model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.Integration.Test.Mocks
{
    public class AzureServiceBusMessageBusMock : IMessageBus
    {
        private readonly string? _connectionString;

        public AzureServiceBusMessageBusMock(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
        }

        public async Task PublishMessage(EntityBase message, string topicName)
        {
            await Task.CompletedTask;
        }
    }
}
