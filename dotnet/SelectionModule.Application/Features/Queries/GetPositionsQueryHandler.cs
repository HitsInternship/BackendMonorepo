using AutoMapper;
using MediatR;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Queries;

public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, List<PositionDto>>
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _positionRepository;

    public GetPositionsQueryHandler(IMapper mapper, IPositionRepository positionRepository)
    {
        _mapper = mapper;
        _positionRepository = positionRepository;
    }

    public async Task<List<PositionDto>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<PositionDto>>(await _positionRepository.GetAllAsync());
    }
}