using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using Twisted.Domain.Interfaces;
using Twisted.Domain.Services;
using Twisted.Infrastructure.ExternalAPIs;
using Twisted.Infrastructure.Interfaces;
using Twisted.Infrastructure.Repositories;
using Twisted.UseCases.Handlers;

namespace Twisted.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTwistedServices(this IServiceCollection services)
        {
            services.AddHttpClient<IBnetOAuthAPI, BnetOAuthAPI>();
            services.AddHttpClient<IBnetWoWRetailAPI, BnetWoWRetailAPI>(client =>
            {
                client.BaseAddress = new Uri("https://us.api.blizzard.com"); // TODO: I should probably set this endpoint in the appsettings.json but I can do it later
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddScoped<IBnetAPIServices, BnetAPIServices>();
            services.AddScoped<ICredentialsRepository, CredentialsRepository>();
            services.AddScoped<IBnetAPIHandler, BnetAPIHandler>();
            services.AddScoped<IEncryptionHandler, EncryptionHandler>();
            return services;
        }

        public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            var identitySection = configuration.GetSection("Identity");

            if (!identitySection.Exists())
            {
                return services; //no authentication identity field so return the services
            }

            return services;
        }
    }
}
