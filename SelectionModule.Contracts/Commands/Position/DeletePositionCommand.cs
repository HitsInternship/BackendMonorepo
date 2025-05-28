using MediatR;

namespace SelectionModule.Contracts.Commands.Position;

public record DeletePositionCommand( Guid PositionId) : IRequest<Unit>;