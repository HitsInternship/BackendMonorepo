using MediatR;
using SelectionModule.Contracts.Commands.Position;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;

namespace SelectionModule.Application.Features.Commands.Position;

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Unit>
{
    private readonly IPositionRepository _positionRepository;

    public CreatePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<Unit> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        await _positionRepository.AddAsync(new PositionEntity
        {
            Name = request.PositionRequestDto.Name,
            Description = request.PositionRequestDto.Description,
        });
        
        return Unit.Value;
    }
}