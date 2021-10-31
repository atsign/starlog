using System;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StarLog.Data;
using StarLog.Options;

namespace StarLog.Extensions
{
    public static class ServiceCollectionCosmosDbExtensions
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider => {
                var connectionStringOptions =
                    serviceProvider.GetRequiredService<IOptions<ConnectionStringOptions>>();

                var serviceEndpoint = connectionStringOptions.Value?.ServiceEndpoint
                    ?? throw new ArgumentException(nameof(ConnectionStringOptions.ServiceEndpoint));
                var authKey = connectionStringOptions.Value?.AuthKey
                    ?? throw new ArgumentException(nameof(ConnectionStringOptions.AuthKey));

                return new CosmosClientBuilder(serviceEndpoint.ToString(), authKey)
                    .WithSerializerOptions(new CosmosSerializationOptions
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    })
                    .Build();
            });

            services.AddSingleton<ICosmosDbRepositoryFactory, CosmosDbRepositoryFactory>();

            return services;
        }
    }
}