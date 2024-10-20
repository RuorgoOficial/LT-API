using LT.model.Commands.Queries;
using LT.model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LT.Integration.Test.Mocks
{
    public class RabbitMQConsumerMock<T> : BackgroundService
        where T : EntityBaseDto
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;

        public RabbitMQConsumerMock(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e, CancellationToken cancellationToken)
        {
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            var entity = JsonConvert.DeserializeObject<T>(content);
            var command = new InsertCommand<T>(entity!);

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var response = await mediator.Send(command, cancellationToken);
        }
    }
}
