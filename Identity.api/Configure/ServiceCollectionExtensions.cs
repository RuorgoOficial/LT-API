using Asp.Versioning;
using AutoMapper;
using LT.dal.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LT.model;
using LT.dal.Abstractions;
using LT.dal.Access;
using Identity.api.Services;

namespace Identity.api.Configure
{
    public static class ServiceCollectionExtensions
    {
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

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
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

            return services;
        }
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration);
            services.AddScoped<ConnectionStrings>();

            services.Configure<JwtSettings>(configuration);
            services.AddScoped<JwtSettings>();

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

    }
}
