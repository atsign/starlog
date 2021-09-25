using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StarLog.Extensions.MapperConfigurations;

namespace StarLog.Extensions
{
    public static class ServiceCollectionMapperConfigurations
    {
        public static void AddMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(serviceProvider => {
                var cfg = new MapperConfiguration(cfg => {
                    cfg.AddCelestialObjectMapperConfiguration();
                });

                return cfg.CreateMapper();
            });
        }
    }
}