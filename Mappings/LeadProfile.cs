using AutoMapper;
using technical_tests_backend_ssr.Models;

namespace tecnom_technical_test.Mappings
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            // Mapeo bidireccional entre Lead y LeadDto, incluyendo Id
            CreateMap<Lead, LeadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => MapServiceType(src.ServiceType)))
                .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));

            CreateMap<LeadDto, Lead>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => MapServiceTypeReverse(src.ServiceType)))
                .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));

            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }

        private static string MapServiceType(ServiceType serviceType)
        {
            return serviceType switch
            {
                ServiceType.CambioAceite => "cambio_aceite",
                ServiceType.RotacionNeumaticos => "rotacion_neumaticos",
                ServiceType.Otro => "otro",
                _ => "otro"
            };
        }

        private static ServiceType MapServiceTypeReverse(string serviceType)
        {
            return serviceType switch
            {
                "cambio_aceite" => ServiceType.CambioAceite,
                "rotacion_neumaticos" => ServiceType.RotacionNeumaticos,
                "otro" => ServiceType.Otro,
                _ => ServiceType.Otro
            };
        }
    }
}
