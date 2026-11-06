using Twisted.Infrastructure.ExternalAPIs;
using Twisted.Infrastructure.Interfaces;

namespace Twisted.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTwistedServices(this IServiceCollection services)
        {
            services.AddScoped<IBnetAPI, BnetAPI>();
            return services;
        }
    }
}
