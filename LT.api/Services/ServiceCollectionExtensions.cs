using LT.dal.Context;
using LT.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace LT.api.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(
             this IServiceCollection services)
        {
            services.AddScoped<LT.core.BaseCore<LT.dal.Access.ScoreDal, EntityScore>>();
            services.AddScoped<LT.core.BaseCore<LT.dal.Access.TestDal, EntityTest>>();
            services.AddScoped<LT.core.ScoreCore>();
            services.AddScoped<LT.core.TestCore>();

            return services;
        }

        public static IServiceCollection AddDal(
             this IServiceCollection services)
        {
            services.AddScoped<LT.dal.Access.BaseDal<EntityScore>>();
            services.AddScoped<LT.dal.Access.BaseDal<EntityTest>>();
            services.AddScoped<LT.dal.Access.ScoreDal>();
            services.AddScoped<LT.dal.Access.TestDal>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(
             this IServiceCollection services)
        {

            services.AddDbContext<LTContext>();
            services.AddDbContext<LT.dal.Context.IdentityDbContext>();

            return services;
        }

        public static IServiceCollection AddIdentity(
             this IServiceCollection services)
        {

            services.AddAuthorization();
            services.AddIdentityApiEndpoints<IdentityUser>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<LT.dal.Context.IdentityDbContext>();

            return services;
        }

        public static IServiceCollection AddConfig(
             this IServiceCollection services)
        {
            //services.AddScoped<ConfigurationBuilder>();
            //configuration.GetConnectionString("LTContext");
            //configuration.GetConnectionString("IdentityDbContext");

            return services;
        }
    }
}
