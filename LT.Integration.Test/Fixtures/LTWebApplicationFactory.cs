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
using LT.model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace LT.Integration.Test
{
    public class LTWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly DatabaseFixture _database;
        public LTWebApplicationFactory(DatabaseFixture database)
        {
            _database = database;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSetting("ConnectionStrings:LTContext", _database.ConnectionString);
            builder.ConfigureServices(s => {

                s.AddScoped<ILTUnitOfWork, LTUnitOfWork>();
                s.AddScoped(typeof(ILTRepository<EntityScore>), typeof(BaseDal<EntityScore>));
                s.AddScoped(typeof(ILTRepository<EntityScore>), typeof(ScoreDal));
                s.AddScoped(typeof(ILTRepository<EntityTest>), typeof(BaseDal<EntityTest>));
                s.AddScoped(typeof(ILTRepository<EntityTest>), typeof(TestDal));
                s.AddScoped<BaseDal<EntityScore>>();
                s.AddScoped<BaseDal<EntityTest>>();
                s.AddScoped<ScoreDal>();
                s.AddScoped<TestDal>();

                s.AddApplication();
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
            });
        }
    }
}
