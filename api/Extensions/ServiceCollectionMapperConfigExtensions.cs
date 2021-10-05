using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StarLog.Entities;
using StarLog.Extensions.MapperConfigurations;
using StarLog.Models;

namespace StarLog.Extensions
{
    public static class ServiceCollectionMapperConfigurations
    {
        public static void AddMapperConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(serviceProvider => {
                var cfg = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Observation, ObservationModel>()
                        .ReverseMap();
                    cfg.AddCelestialObjectMapperConfiguration();
                });

                return cfg.CreateMapper();
            });
        }
    }
}