using Asp.Versioning;
using AutoMapper;
using LT.core.Services;
using LT.dal;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using LT.model.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using System.Data.Common;
using System.Reflection;

namespace LT.Identity.api.Configure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(
             this IServiceCollection services)
        {
            services.AddScoped<core.BaseCore<ScoreDal, EntityScore>>();
            services.AddScoped<core.BaseCore<TestDal, EntityTest>>();
            services.AddScoped<core.ScoreCore>();
            services.AddScoped<core.TestCore>();

            return services;
        }
        public static IServiceCollection AddDal(
             this IServiceCollection services)
        {
            services.AddScoped<ILTUnitOfWork, LTUnitOfWork>();

            services.AddScoped(typeof(ILTRepository<EntityScore>), typeof(BaseDal<EntityScore>));
            services.AddScoped(typeof(ILTRepository<EntityScore>), typeof(ScoreDal));
            services.AddScoped<BaseDal<EntityScore>>();
            services.AddScoped<BaseDal<EntityTest>>();
            services.AddScoped<ScoreDal>();
            services.AddScoped<TestDal>();

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
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   //IssuerSigningKey = new SymmetricSecurityKey("LT"),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            return services;
        }
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration);
            services.AddScoped<ConnectionStrings>();

            services.Configure<AppSettings>(configuration);
            services.AddScoped<AppSettings>();

            return services;
        }
        public static IServiceCollection AddDatabaseContext(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LTDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LTContext")));
            services.AddDbContext<LoggerDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("LoggerDBContext")));
            services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("IdentityDbContext")));

            return services;
        }
        public static IServiceCollection AddApiVersioningService(
             this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
        public static IServiceCollection AddMediatRAssemblies(this IServiceCollection services)
        {
            services.AddApplication();

            return services;
        }

    }
}
