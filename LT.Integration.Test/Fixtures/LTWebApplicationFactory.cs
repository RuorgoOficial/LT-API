using AutoFixture;
using LT.core;
using LT.dal.Abstractions;
using LT.model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
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
        }
    }
}
