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
                .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id.HasValue))
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore());

        }
    }
}
