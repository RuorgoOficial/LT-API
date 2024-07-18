using Asp.Versioning;
using AutoMapper;
using LT.api.Controllers;
using LT.api.Metrics;
using LT.api.Services;
using LT.dal;
using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using LT.model.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using OpenTelemetry.Metrics;
using System.Data.Common;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LT.messageBus;
using System.Net.Security;
using LT.core.RabbitMQSender;
using LT.core.RabbitMQConsumer;

namespace LT.api.Configure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(
             this IServiceCollection services)
        {
            //services.AddScoped<core.BaseCore<ScoreDal, EntityScore>>();
            //services.AddScoped<core.BaseCore<TestDal, EntityTest>>();
            //services.AddScoped<core.ScoreCore>();
            //services.AddScoped<core.TestCore>();

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
             this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization();

            return services;
        }
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration);
            services.AddScoped<ConnectionStrings>();

            services.Configure<JwtSettings>(configuration);
            services.AddScoped<JwtSettings>();

            services.Configure<RabbitMQSettings>(configuration);
            services.AddScoped<RabbitMQSettings>();

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
        public static IServiceCollection AddAutoMapper(
             this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
        public static IServiceCollection AddMediatRAssemblies(this IServiceCollection services)
        {
            services.AddApplication();

            return services;
        }

        public static IServiceCollection AddServiceBusConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LTDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LTContext"));

            var context = new LTDBContext(optionsBuilder.Options);
            services.AddSingleton(new ScoreDal(context));
            services.AddSingleton(new LTUnitOfWork(context));
            services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

            services.AddScoped<IMessageBus, AzureServiceBusMessageBus>();

            return services;
        }
        public static IServiceCollection AddRabbitMQSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
            
            services.AddHostedService<RabbitMQConsumer<EntityScoreDto>>();

            return services;
        }

        public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHttpRepository<EntityScoreDto>, ScoreHttpRepository>();
            services.AddHttpClient<IHttpRepository<EntityScoreDto>, ScoreHttpRepository>(u => 
                u.BaseAddress = new Uri(configuration["ServiceUrls:ScoreApi"]!)
            );

            return services;
        }

    }
}
