using AutoMapper;
using MediatR;
using SelectionModule.Contracts.Commands.Position;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Application.Features.Commands.Position;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, PositionDto>
{
    private readonly IMapper _mapper;
    private readonly IPositionRepository _positionRepository;

    public CreatePositionCommandHandler(IPositionRepository positionRepository, IMapper mapper)
    {
        _positionRepository = positionRepository;
        _mapper = mapper;
    }

    public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = new PositionEntity
        {
            Name = request.PositionRequestDto.Name,
            Description = request.PositionRequestDto.Description,
        };
        
        await _positionRepository.AddAsync(position);
        
        return _mapper.Map<PositionDto>(position);
    }
}