using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarLog.Extensions;
using StarLog.Options;

[assembly: FunctionsStartup(typeof(StarLog.Startup))]
namespace StarLog
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath,
                    $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }

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
        }
    }
}