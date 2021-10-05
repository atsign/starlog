using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarLog.Extensions;
using StarLog.Options;
using StarLog.Services;

[assembly: FunctionsStartup(typeof(StarLog.Startup))]
namespace StarLog
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<ConnectionStringOptions>()
                .Configure<IConfiguration>((settings, configuration) => {
                    configuration.GetSection(Constants.Configuration.ConnectionStringOptions)
                        .Bind(settings);
                });
            
            builder.Services.AddOptions<CosmosDbOptions>()
                .Configure<IConfiguration>((settings, configuration) => {
                    configuration.GetSection(Constants.Configuration.CosmosDbOptions)
                        .Bind(settings);
                });
            
            builder.Services.AddCosmosDb();
            builder.Services.AddMapperConfigurations();

            builder.Services.AddScoped<ICelestialObjectService, CelestialObjectService>();
            builder.Services.AddScoped<IObservationService, ObservationService>();
        }
    }
}