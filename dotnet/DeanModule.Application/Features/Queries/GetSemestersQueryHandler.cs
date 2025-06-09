using AutoMapper;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeanModule.Application.Features.Queries;

public class GetSemestersQueryHandler : IRequestHandler<GetSemestersQuery, List<SemesterResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ISemesterRepository _semesterRepository;

    public GetSemestersQueryHandler(IMapper mapper, ISemesterRepository semesterRepository)
    {
        _mapper = mapper;
        _semesterRepository = semesterRepository;
    }

    public async Task<List<SemesterResponseDto>> Handle(GetSemestersQuery request, CancellationToken cancellationToken)
    {
        if (request.IsArchive)
        {
            var archivedSemesters = await _semesterRepository.ListAllArchivedAsync();
            var archivedSemesterList = await archivedSemesters.ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<SemesterResponseDto>>(archivedSemesterList);
        }

        var semesters = await _semesterRepository.ListAllActiveAsync();
        var semesterList = await semesters.ToListAsync(cancellationToken: cancellationToken);
        return _mapper.Map<List<SemesterResponseDto>>(semesterList);
    }
}