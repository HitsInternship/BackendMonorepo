using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Repositories;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Configs;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, ApplicationsDto>
{
    private readonly int _size;
    private readonly IMapper _mapper;
    private readonly IPositionRepository _positionRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationsQueryHandler(IApplicationRepository applicationRepository, IOptions<PaginationConfig> config,
        IMapper mapper, IStudentRepository studentRepository, ICompanyRepository companyRepository,
        IPositionRepository positionRepository)
    {
        _size = config.Value.PageSize;
        _applicationRepository = applicationRepository;
        _mapper = mapper;
        _studentRepository = studentRepository;
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
    }

    public async Task<ApplicationsDto> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * _size;

        var query = request.IsArchives
            ? await _applicationRepository.ListAllArchivedAsync()
            : await _applicationRepository.ListAllAsync();

        if (request.ApplicationStatus != null)
            query = query.Where(x => x.Status == request.ApplicationStatus);

        if (request.StudentId != null)
            query = query.Where(x => x.StudentId == request.StudentId);

        var totalCount = await query.CountAsync(cancellationToken);

        var pagedApplications = await query
            .Skip(skip)
            .Take(_size)
            .ToListAsync(cancellationToken);

        var result = new List<ListedApplicationResponseDto>();
        
        foreach (var application in pagedApplications)
        {
            var applicationDto = _mapper.Map<ListedApplicationResponseDto>(application);
            applicationDto.Position =
                _mapper.Map<PositionDto>(await _positionRepository.GetByIdAsync(application.PositionId));
            applicationDto.Company =
                _mapper.Map<CompanyResponse>(await _companyRepository.GetByIdAsync(application.CompanyId));
            applicationDto.Student =
                _mapper.Map<StudentDto>(await _studentRepository.GetByIdAsync(application.StudentId));
            
            result.Add(applicationDto);
        }
        
        return new ApplicationsDto(result, _size, totalCount, request.Page);
    }
}