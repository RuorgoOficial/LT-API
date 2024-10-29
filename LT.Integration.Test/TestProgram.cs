using AutoMapper;
using FluentAssertions.Common;
using LT.api.Application.Handlers;
using LT.api.Configure;
using LT.api.Services;
using LT.core.RabbitMQSender;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.Integration.Test.Mocks;
using LT.messageBus;
using LT.model;
using LT.model.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatRAssemblies();

builder.Services.AddDal();

builder.Services.AddDbContext<LTDBContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
builder.Services.AddDbContext<LoggerDBContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
builder.Services.AddDbContext<IdentityDbContext>(opt => opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//Mock this data.
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSenderMock>();
builder.Services.AddHostedService<RabbitMQConsumerMock<EntityScoreDto>>();
builder.Services.AddScoped<IHttpRepository<EntityScoreDto>, ScoreHttpRepositoryMock>();
builder.Services.AddHttpClient<IHttpRepository<EntityScoreDto>, ScoreHttpRepositoryMock>();

var optionsBuilder = new DbContextOptionsBuilder<LTDBContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("LTContext"));

var context = new LTDBContext(optionsBuilder.Options);
builder.Services.AddSingleton(new ScoreDal(context));
builder.Services.AddSingleton(new LTUnitOfWork(context));
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumerMock>();

builder.Services.AddScoped<IMessageBus, AzureServiceBusMessageBusMock>();

var app = builder.Build();

public partial class TestProgram()
{

}