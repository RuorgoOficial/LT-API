using AutoFixture;
using Azure.Messaging.ServiceBus;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
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
    public class AzureServiceBusConsumerMock : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly ScoreDal _dal;
        private readonly LTUnitOfWork _unitOfWork;
        private ServiceBusProcessor processor;

        public AzureServiceBusConsumerMock(IConfiguration configuration, ScoreDal dal, LTUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _dal = dal;
            _unitOfWork = unitOfWork;

            var connectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
            var topic = _configuration.GetSection("SubscriptionName").Value;
            var subscriptionName = _configuration.GetSection("ServiceBusConnectionString").Value;

            var client = new ServiceBusClient(connectionString);

            processor = client.CreateProcessor(topic, subscriptionName);
        }

        public async Task Start()
        {
            await Task.CompletedTask;
        }

        public async Task Stop()
        {
            await Task.CompletedTask;
        }

        public Task ErrorHandler(ProcessErrorEventArgs errorArgs)
        {
            return Task.CompletedTask;
        }

        public async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            await Task.CompletedTask;
        }
    }
}
