using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Enums;
using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationQueryHandler : IRequestHandler<GetApplicationQuery, ApplicationResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationQueryHandler(IMapper mapper, IApplicationRepository applicationRepository,
        ICompanyRepository companyRepository, IPositionRepository positionRepository,
        IStudentRepository studentRepository)
    {
        _mapper = mapper;
        _applicationRepository = applicationRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _studentRepository = studentRepository;
    }

    public async Task<ApplicationResponseDto> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        if (!await _applicationRepository.CheckIfExistsAsync(request.ApplicationId))
            throw new ApplicationNotFound(request.ApplicationId);

        var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);
        var student = await _studentRepository.GetByIdAsync(application.StudentId);

        if (!request.Roles.Contains("DeanMember"))
        {
            if (student.UserId != request.UserId)
                throw new Forbidden("You are not allowed to access this page");

            if (application.IsDeleted)
                throw new BadRequest("The application is deleted");
        }

        //todo: get old practice
        
        var dto = _mapper.Map<ApplicationResponseDto>(application);

        dto.NewCompany = _mapper.Map<CompanyResponse>(await _companyRepository.GetByIdAsync(application.CompanyId));
        dto.NewPosition = _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(application.PositionId));
        dto.Student = _mapper.Map<StudentDto>(await _studentRepository.GetByIdAsync(application.StudentId));
        dto.OldCompany = new CompanyResponse
        {
            id = Guid.Empty,
            name = "Старая компания",
            description = "Описание",
            status = CompanyStatus.Partner
        };
        dto.OldPosition = new PositionDto
        {
            Id = Guid.Empty,
            IsDeleted = false,
            Name = "Старая позиция",
            Description = "Описание"
        };

        
        return dto;
    }
}