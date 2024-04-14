using LT.api.Metrics;
using LT.dal.Context;
using OpenTelemetry.Metrics;

namespace LT.api.Configure
{
    public static class WebApplicationBuilderExtensions
    {

        public static WebApplicationBuilder AddOpenTelemetryHealthChecks(
             this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<LTDBContext>()
                .AddDbContextCheck<IdentityDbContext>()
                .AddDbContextCheck<LoggerDBContext>();
            builder.Services.AddOpenTelemetry()
                .WithMetrics(builder =>
                {
                    builder.AddPrometheusExporter();

                    builder.AddMeter("Microsoft.AspNetCore.Hosting",
                                        "Microsoft.AspNetCore.Server.Kestrel");
                    builder.AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
                        0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
                        });
                });
            builder.Services.AddSingleton<GetMetrics>();
            return builder;
        }
    }
}
