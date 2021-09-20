using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarLog.Options;

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
        }
    }
}