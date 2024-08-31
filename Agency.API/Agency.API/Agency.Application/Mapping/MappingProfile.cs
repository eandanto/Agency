using AutoMapper;
using Agency.Application.DTOs;
using Agency.Domain.Entities;

namespace Agency.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from UserDto to User entity
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id.HasValue));

            // Map from User entity to UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            // Map from LoginDto to User entity
            CreateMap<LoginDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())  
                .ForMember(dest => dest.LastName, opt => opt.Ignore())   
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Map OffDayDto to OffDay entity
            CreateMap<OffDayDto, OffDay>()
                .ReverseMap();

            // Map from Appointment entity to AppointmentDto
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.CustomerEmailAddress,
                           opt => opt.MapFrom(src => src.Customer != null ? src.Customer.EmailAddress : null));

            // Reverse map if needed (from AppointmentDto to Appointment)
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore());

            // Map from ConfigurationDto to Configuration entity
            CreateMap<ConfigurationDto, Configuration>()
                .ReverseMap();
        }
    }
}
