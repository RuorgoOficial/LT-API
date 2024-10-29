using AutoFixture;
using AutoMapper;
using FluentAssertions.Common;
using LT.api.Configure;
using LT.api.Services;
using LT.core;
using LT.core.RabbitMQConsumer;
using LT.core.RabbitMQSender;
using LT.dal;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.Integration.Test.Mocks;
using LT.messageBus;
using LT.model;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace LT.Integration.Test
{
    public class LTWebApplicationFactory : WebApplicationFactory<TestProgram>
    {
        private readonly DatabaseFixture _database;
        public LTWebApplicationFactory(DatabaseFixture database)
        {
            _database = database;
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("ConnectionStrings:LTContext", _database.ConnectionString);
            builder.ConfigureServices(s =>
            {
                s.AddMediatRAssemblies();
                s.AddDal();

                s.AddDbContext<LTDBContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
                s.AddDbContext<LoggerDBContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
                s.AddDbContext<IdentityDbContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });

                IMapper mapper = mapperConfig.CreateMapper();
                s.AddSingleton(mapper);

                //Mock this data.
                s.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSenderMock>();
                s.AddHostedService<RabbitMQConsumerMock<EntityScoreDto>>();
                s.AddScoped<IHttpRepository<EntityScoreDto>, ScoreHttpRepositoryMock>();
                s.AddHttpClient<IHttpRepository<EntityScoreDto>, ScoreHttpRepositoryMock>();

                var optionsBuilder = new DbContextOptionsBuilder<LTDBContext>();
                optionsBuilder.UseSqlServer(_database.ConnectionString);

                var context = new LTDBContext(optionsBuilder.Options);
                s.AddSingleton(new ScoreDal(context));
                s.AddSingleton(new LTUnitOfWork(context));
                s.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumerMock>();

                s.AddScoped<IMessageBus, AzureServiceBusMessageBusMock>();
            }
            );

        }
    }
}
