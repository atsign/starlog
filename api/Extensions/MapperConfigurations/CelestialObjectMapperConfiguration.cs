using AutoMapper;
using StarLog.Entities;
using StarLog.Models;

namespace StarLog.Extensions.MapperConfigurations
{
    public static class CelestialObjectMapperConfiguration
    {
        public static IMapperConfigurationExpression AddCelestialObjectMapperConfiguration(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CelestialObject, CelestialObjectModel>()
                .ForMember(dest => dest.Declination, opts => opts.MapFrom(src => src.Dec))
                .ForMember(dest => dest.RightAscension, opts => opts.MapFrom(src => src.RA))
                .ForMember(dest => dest.CommonNames, opts =>
                    opts.MapFrom(src =>
                        src.CommonNames.Length > 0
                            ? src.CommonNames.Split(",", System.StringSplitOptions.None)
                            : null as string[]
                        ));
            
            return cfg;
        }
    }
}