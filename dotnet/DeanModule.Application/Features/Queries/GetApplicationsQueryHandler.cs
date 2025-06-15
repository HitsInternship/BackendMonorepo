using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Enums;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Configs;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Enums;
using UserModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, ApplicationsDto>
{
    private readonly int _size;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationsQueryHandler(IApplicationRepository applicationRepository, IOptions<PaginationConfig> config,
        IMapper mapper, IStudentRepository studentRepository, ICompanyRepository companyRepository,
        IPositionRepository positionRepository, IUserRepository userRepository)
    {
        _size = config.Value.PageSize;
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _studentRepository = studentRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _userRepository = userRepository;
    }

    public async Task<ApplicationsDto> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page <= 0) throw new BadRequest("Page must be greater than 0");

        var skip = (request.Page - 1) * _size;

        var query = request.IsArchives
            ? await _applicationRepository.ListAllArchivedAsync()
            : await _applicationRepository.ListAllActiveAsync();

        if (request.Roles.Contains("Student"))
        {
            var students = await _studentRepository.GetStudentByUserIdAsync(request.UserId);

            query = query.Where(x => x.StudentId == students.Id);
        }
        else
        {
            if (request.ApplicationStatus != null)
                query = query.Where(x => x.Status == request.ApplicationStatus);

            if (request.StudentId != null)
                query = query.Where(x => x.StudentId == request.StudentId);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _size));

        if (request.Page > totalPages) throw new BadRequest("Page must be less than or equal to the number of items");
        
        var pagedApplications = await query
            .Skip(skip)
            .Take(_size)
            .ToListAsync(cancellationToken);

        var result = new List<ListedApplicationResponseDto>();

        //todo: get old practice
        
        foreach (var application in pagedApplications)
        {
            var student = await _studentRepository.GetStudentByIdAsync(application.StudentId);
            var user = await _userRepository.GetByIdAsync(student.UserId);

            var applicationDto = _mapper.Map<ListedApplicationResponseDto>(application);
            applicationDto.NewPosition =
                _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(application.PositionId));
            applicationDto.NewCompany =
                _mapper.Map<CompanyResponse>(await _companyRepository.GetByIdAsync(application.CompanyId));
            applicationDto.OldCompany = new CompanyResponse
            {
                id = Guid.Empty,
                name = "Старая компания",
                description = "Описание",
                status = CompanyStatus.Partner
            };
            applicationDto.OldPosition = new PositionDto
            {
                Id = Guid.Empty,
                IsDeleted = false,
                Name = "Старая позиция",
                Description = "Описание"
            };
            applicationDto.Student = new StudentDto(student)
            {
                Name = user.Name,
                Email = user.Email,
                Surname = user.Surname
            };

            result.Add(applicationDto);
        }

        return new ApplicationsDto(result, _size, totalPages, request.Page);
    }
}