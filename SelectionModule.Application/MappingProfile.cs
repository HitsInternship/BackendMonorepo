using AutoMapper;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PositionEntity, PositionDto>();
        CreateMap<VacancyEntity, ListedVacancyDto>();
        CreateMap<VacancyEntity, VacancyDto>();
    }
}