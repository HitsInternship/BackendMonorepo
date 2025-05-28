using MediatR;
using SelectionModule.Contracts.Commands.Position;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.Position;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Unit>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IVacancyRepository _vacancyRepository;

    public DeletePositionCommandHandler(IPositionRepository positionRepository, IVacancyRepository vacancyRepository)
    {
        _positionRepository = positionRepository;
        _vacancyRepository = vacancyRepository;
    }

    public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        if (!await _positionRepository.CheckIfExistsAsync(request.PositionId))
            throw new NotFound("Position not found");

        var position = await _positionRepository.GetByIdAsync(request.PositionId);

        if ((await _vacancyRepository.FindAsync(x => x.PositionId == position.Id)).Any())
            throw new BadRequest("You cannot delete this position, because there are vacancies with this position.");
        
        await _positionRepository.DeleteAsync(position);
        
        return Unit.Value; 
    }
}