using AutoMapper;
using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Domain.Entities;

namespace CompanyModule.Application;

public class CompanyModuleMappingProfile : Profile
{
    public CompanyModuleMappingProfile()
    {
        CreateMap<CompanyRequest, Company>();
        CreateMap<EditCompanyRequest, Company>();
        CreateMap<Company, CompanyResponse>();
        CreateMap<Company, ShortenCompanyDto>();
        
        CreateMap<PartnershipAgreementRequest, PartnershipAgreement>();
        CreateMap<PartnershipAgreement, PartnershipAgreementResponse>();

        CreateMap<CuratorRequest, Curator>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.userRequest));

        CreateMap<Curator, CuratorResponse>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.surname, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.companyId, opt => opt.MapFrom(src => src.Company.Id))
            .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Company.Name));

        CreateMap<AppointmentRequest, Appointment>();
        CreateMap<Appointment, AppointmentResponse>()
            .ForMember(dest => dest.documentIds, opt => opt.MapFrom(src => src.Attachments.Select(attachment => attachment.DocumentId)))
            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Timeslot.Date.ToLocalTime()))
            .ForMember(dest => dest.periodNumber, opt => opt.MapFrom(src => src.Timeslot.PeriodNumber));
        CreateMap<Appointment, ShortenAppointmentResponse>()
            .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Company.Name));

        CreateMap<TimeslotRequest, Timeslot>();
        CreateMap<Timeslot, TimeslotResponse>();
    }
}