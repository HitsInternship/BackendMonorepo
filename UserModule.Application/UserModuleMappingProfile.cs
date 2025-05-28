using AutoMapper;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Contracts.DTOs.Responses;
using UserModule.Domain.Entities;

namespace UserModule.Application;

public class UserModuleMappingProfile : Profile
{
    public UserModuleMappingProfile()
    {
        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.roles,
                       opt => opt.MapFrom(src => src.Roles.Select(role => role.RoleName).ToList()));
    }
}