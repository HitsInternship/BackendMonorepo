using AutoMapper;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.DTOs.Responses;
using PracticeModule.Domain.Entity;

namespace CompanyModule.Application;

public class PracticeModuleMappingProfile : Profile
{
    public PracticeModuleMappingProfile()
    {
        CreateMap<Practice, PracticeResponse>()
            .ForMember(response => response.studentFullName, opt => opt.MapFrom(model => $"{model.Student.User.Surname} {model.Student.User.Name} {model.Student.Middlename}"))
            .ForMember(response => response.practiceDiaryId, opt => opt.MapFrom(model => model.PracticeDiary.Id))
            .ForMember(response => response.characteristicsId, opt => opt.MapFrom(model => model.StudentPracticeCharacteristics.Id));

        CreateMap<Practice, PotentialPracticeResponse>()
            .ForMember(response => response.studentId, opt => opt.MapFrom(model => model.Student.Id))
            .ForMember(response => response.studentFullName, opt => opt.MapFrom(model => $"{model.Student.User.Surname} {model.Student.User.Name} {model.Student.Middlename}"))
            
            .ForMember(response => response.companyId, opt => opt.MapFrom(model => model.Company.Id))
            .ForMember(response => response.companyName, opt => opt.MapFrom(model => model.Company.Name))
            .ForMember(response => response.positionId, opt => opt.MapFrom(model => model.Position.Id))
            .ForMember(response => response.positionName, opt => opt.MapFrom(model => model.Position.Name))

            .ForMember(response => response.newCompanyId, opt => opt.MapFrom(model => model.NewCompany.Id))
            .ForMember(response => response.newCompanyName, opt => opt.MapFrom(model => model.NewCompany.Name))
            .ForMember(response => response.newPositionId, opt => opt.MapFrom(model => model.NewPosition.Id))
            .ForMember(response => response.newPositionName, opt => opt.MapFrom(model => model.NewPosition.Name));

        CreateMap<CreateGlobalPracticeRequest, GlobalPractice>();
        CreateMap<GlobalPractice, GlobalPracticeResponse>();
    }
}