using AutoMapper;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Entities;

namespace StudentModule.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StudentEntity, StudentDto>();
        CreateMap<StreamEntity, StreamDto>();
    }
}