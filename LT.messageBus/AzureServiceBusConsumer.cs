using Azure.Messaging.ServiceBus;
using LT.dal.Access;
using LT.dal.Context;
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
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly ScoreDal _dal;
        private readonly LTUnitOfWork _unitOfWork;
        private ServiceBusProcessor processor;

        public AzureServiceBusConsumer(IConfiguration configuration, ScoreDal dal, LTUnitOfWork unitOfWork)
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
            processor.ProcessMessageAsync += OnCheckoutMessageReceived;
            processor.ProcessErrorAsync += ErrorHandler;   

            await processor.StopProcessingAsync();
        }

        public async Task Stop()
        {
            await processor.StopProcessingAsync();
            await processor.DisposeAsync();
        }

        public Task ErrorHandler(ProcessErrorEventArgs errorArgs)
        {
            Console.WriteLine(errorArgs.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            string body = Encoding.UTF8.GetString(message.Body);
            EntityScore entity = JsonConvert.DeserializeObject<EntityScore>(body)!;
            if (entity is null)
                throw new ArgumentNullException(nameof(body));

            await _dal.Add(entity);
            await _unitOfWork.SaveChangesAsync(new CancellationToken());
        }
    }
}
