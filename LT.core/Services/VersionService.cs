using LT.dal.Abstractions;
using LT.model.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core.Services
{
    public static class VersionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssemblies(typeof(VersionExtensions).Assembly));
            return services;
        }
    }
}
