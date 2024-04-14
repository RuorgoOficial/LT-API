using Asp.Versioning.ApiExplorer;
using LT.api.Metrics;
using LT.dal.Context;
using OpenTelemetry.Metrics;

namespace LT.api.Configure
{
    public static class WebApplicationExtensions
    {
        public static WebApplication AddSwagger(
             this WebApplication builder)
        {
            var apiVersionDescriptionProvider = builder.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            }
            );
            return builder;
        }
    }
}
