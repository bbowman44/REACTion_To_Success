using Twisted.Infrastructure.ExternalAPIs;

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
