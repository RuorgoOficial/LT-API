using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using LT.api.Configure;
using LT.api.Metrics;
using LT.dal.Context;
using Microsoft.AspNetCore.Identity;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddDal();
builder.Services.AddIdentity();
builder.Services.AddConfig(builder.Configuration);
builder.AddOpenTelemetryHealthChecks();
builder.Services.AddApiVersioningService();
builder.Services.AddAutoMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.AddSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapHealthChecks("/healthz");

app.MapControllers();

app.Run();
