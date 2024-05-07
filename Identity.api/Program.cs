using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Identity.api.Configure;
using LT.dal.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatRAssemblies();
builder.Services.AddDal();
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddRazorPages();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapRazorPages();
});

app.MapControllers();

app.Run();
