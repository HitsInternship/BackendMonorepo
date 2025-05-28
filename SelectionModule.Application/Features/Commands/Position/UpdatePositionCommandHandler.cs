using MediatR;
using SelectionModule.Contracts.Commands.Position;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Position;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Unit>
{
    private readonly IPositionRepository _positionRepository;

    public UpdatePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<Unit> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        if(!await _positionRepository.CheckIfExistsAsync(request.PositionId))
            throw new NotFound("Position Not Found");
        
        var position = await _positionRepository.GetByIdAsync(request.PositionId);
        
        position.Name = request.PositionRequestDto.Name;
        position.Description = request.PositionRequestDto.Description;
        
        await _positionRepository.UpdateAsync(position);
        
        return Unit.Value;
    }
}