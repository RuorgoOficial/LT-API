using LT.messageBus;
using Microsoft.Extensions.DependencyInjection;

namespace LT.api.Configure
{
    public static class ApplicationBuilderExtensionsServiceBus
    {
        public static IAzureServiceBusConsumer? ServiceBusConsumer { get; set; } 
        public static IApplicationBuilder AddServiceBusConsumer(this IApplicationBuilder builder)
        {
            ServiceBusConsumer = builder.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLifetime = builder.ApplicationServices.GetService<IHostApplicationLifetime>();

            if(hostApplicationLifetime != null)
            {
                hostApplicationLifetime.ApplicationStarted.Register(OnStart);
                hostApplicationLifetime.ApplicationStopped.Register(OnStop);
            }

            return builder;
        }

        private static void OnStart()
        {
            if(ServiceBusConsumer != null)
            {
                ServiceBusConsumer.Start();
            }
        }
        private static void OnStop() 
        {
            if(ServiceBusConsumer != null)
            {
                ServiceBusConsumer.Stop();
            }
        }
    }
}
