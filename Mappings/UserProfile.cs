using AutoMapper;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Password should not be mapped
            CreateMap<UserDto, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Password should not be mapped
        }
    }
}
