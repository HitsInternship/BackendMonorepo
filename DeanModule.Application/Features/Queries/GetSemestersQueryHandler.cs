using AutoMapper;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Contracts.Repositories;
using MediatR;

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
            return _mapper.Map<List<SemesterResponseDto>>(
                await _semesterRepository.ListAllArchivedAsync());
        }

        return _mapper.Map<List<SemesterResponseDto>>(
            await _semesterRepository.ListAllAsync());
    }
}