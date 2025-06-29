using AutoMapper;
using DeanModule.Contracts.Repositories;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;
using Shared.Contracts.Dtos;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class
    GetGlobalSelectionsQueryHandler : IRequestHandler<GetGlobalSelectionsQuery, List<GlobalSelectionDto>>
{
    private readonly IMapper _mapper;
    private readonly IGlobalSelectionRepository _globalSelectionRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IStreamRepository _streamRepository;

    public GetGlobalSelectionsQueryHandler(IMapper mapper, IGlobalSelectionRepository globalSelectionRepository,
        ISemesterRepository semesterRepository, IStreamRepository streamRepository)
    {
        _mapper = mapper;
        _globalSelectionRepository = globalSelectionRepository;
        _semesterRepository = semesterRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<GlobalSelectionDto>> Handle(GetGlobalSelectionsQuery request,
        CancellationToken cancellationToken)
    {
        var globalSelections = (request.IsArchived
            ? await _globalSelectionRepository.ListAllArchivedAsync()
            : await _globalSelectionRepository.ListAllActiveAsync()).ToList();

        var result = new List<GlobalSelectionDto>();

        foreach (var globalSelection in globalSelections)
        {
            result.Add(new GlobalSelectionDto
            {
                Id = globalSelection.Id,
                IsDeleted = globalSelection.IsDeleted,
                Semester = _mapper.Map<SemesterResponseDto>(
                    await _semesterRepository.GetByIdAsync(globalSelection.SemesterId)),
                Stream = _mapper.Map<StreamDto>(await _streamRepository.GetByIdAsync(globalSelection.StreamId)),
            });
        }

        return result;
    }
}