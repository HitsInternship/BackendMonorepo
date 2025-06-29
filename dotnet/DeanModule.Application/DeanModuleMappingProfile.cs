using AutoMapper;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Domain.Entities;
using Shared.Contracts.Dtos;

namespace DeanModule.Application;

public class DeanModuleMappingProfile : Profile
{
    public DeanModuleMappingProfile()
    {
        CreateMap<SemesterRequestDto, SemesterEntity>();
        CreateMap<SemesterEntity, SemesterResponseDto>();
        CreateMap<StreamSemesterEntity, StreamSemesterResponseDto>()
            .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.SemesterEntity));
        CreateMap<StreamSemesterRequestDto, StreamSemesterEntity>();
        CreateMap<ApplicationRequestDto, ApplicationEntity>();
        CreateMap<ApplicationEntity, ApplicationResponseDto>();
        CreateMap<ApplicationEntity, ListedApplicationResponseDto>();
    }
}