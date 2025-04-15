using LT.model.Commands.Queries;
using LT.model;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Channels;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace LT.core.RabbitMQConsumer
{
    public class RabbitMQConsumer<T> : BackgroundService
        where T : EntityBaseDto
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IChannel _channel;
        private string _queueName;

        public RabbitMQConsumer(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;

            _queueName = _configuration.GetSection("RabbitMQSettings:ScoreQueueName").Value!;
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetSection("RabbitMQSettings:HostName").Value!,
                Password = _configuration.GetSection("RabbitMQSettings:Password").Value!,
                UserName = _configuration.GetSection("RabbitMQSettings:UserName").Value!,
            };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            QueueDeclareOk queueDeclareOk = _channel.QueueDeclareAsync(queue: _queueName, false, false, false, arguments: null).Result;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (sender, e) => Consumer_Received(sender, e, cancellationToken);

            await _channel.BasicConsumeAsync(_queueName, false, consumer);

        }
        private async Task Consumer_Received(object? sender, BasicDeliverEventArgs e, CancellationToken cancellationToken)
        {
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            var entity = JsonConvert.DeserializeObject<T>(content);
            var command = new InsertCommand<T>(entity!);

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var response = await mediator.Send(command, cancellationToken);
            if(response > 0)
            {
                await _channel.BasicAckAsync(e.DeliveryTag, false);
            }
        }
    }
}
