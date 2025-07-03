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

public class GetGlobalSelectionQueryHandler : IRequestHandler<GetGlobalSelectionQuery, GlobalSelectionResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IStreamRepository _streamRepository;
    private readonly ISemesterRepository _semesterRepository;
    private readonly IGlobalSelectionRepository _globalSelectionRepository;

    public GetGlobalSelectionQueryHandler(IMediator mediator, IGlobalSelectionRepository globalSelectionRepository,
        IMapper mapper, ISemesterRepository semesterRepository, IStreamRepository streamRepository)
    {
        _mediator = mediator;
        _globalSelectionRepository = globalSelectionRepository;
        _mapper = mapper;
        _semesterRepository = semesterRepository;
        _streamRepository = streamRepository;
    }

    public async Task<GlobalSelectionResponseDto> Handle(GetGlobalSelectionQuery request,
        CancellationToken cancellationToken)
    {
        var globalSelection = await _globalSelectionRepository.GetByIdAsync(request.Id);

        return new GlobalSelectionResponseDto
        {
            Id = globalSelection.Id,
            IsDeleted = globalSelection.IsDeleted,
            Semester = _mapper.Map<SemesterResponseDto>(
                await _semesterRepository.GetByIdAsync(globalSelection.SemesterId)),
            Stream = _mapper.Map<StreamDto>(await _streamRepository.GetByIdAsync(globalSelection.StreamId)),
            Selections = await _mediator.Send(new GetSelectionsQuery(request.Id,request.GroupNumber, request.Status,
                request.SemesterId, request.UserId, request.Roles), cancellationToken)
        };
    }
}