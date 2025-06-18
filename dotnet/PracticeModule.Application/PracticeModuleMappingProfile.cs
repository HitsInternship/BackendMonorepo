using AutoMapper;
using DeanModule.Domain.Entities;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.DTOs.Responses;
using PracticeModule.Domain.Entity;

namespace PracticeModule.Application;

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
        CreateMap<GlobalPractice, GlobalPracticeResponse>()
            .ForMember(response => response.streamNumber, opt => opt.MapFrom(model => model.Stream.StreamNumber));

        CreateMap<IGrouping<SemesterEntity, GlobalPractice>, SemesterPracticeResponse>()
            .ForMember(response => response.semesterId, opt => opt.MapFrom(group => group.Key.Id))
            .ForMember(response => response.semesterStartDate, opt => opt.MapFrom(group => group.Key.StartDate))
            .ForMember(response => response.semesterEndDate, opt => opt.MapFrom(group => group.Key.EndDate))

            .ForMember(response => response.globalPractices, opt => opt.MapFrom(group => group.ToList()));

        CreateMap<IGrouping<SemesterEntity, GlobalPractice>, StudentGlobalPracticeResponse>()
            .ForMember(response => response.semesterId, opt => opt.MapFrom(group => group.Key.Id))
            .ForMember(response => response.semesterStartDate, opt => opt.MapFrom(group => group.Key.StartDate))
            .ForMember(response => response.semesterEndDate, opt => opt.MapFrom(group => group.Key.EndDate))
            .ForMember(response => response.practiceType, opt => opt.MapFrom(group => group.First().PracticeType))
            .ForMember(response => response.diaryPatternDocumentId, opt => opt.MapFrom(group => group.First().DiaryPatternDocumentId))
            .ForMember(response => response.characteristicsPatternDocumentId, opt => opt.MapFrom(group => group.First().CharacteristicsPatternDocumentId))

            .ForMember(response => response.practice, opt => opt.MapFrom(group => group.First().Practices.First()));
    }
}