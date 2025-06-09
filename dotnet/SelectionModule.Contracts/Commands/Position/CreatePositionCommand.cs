using MediatR;
using SelectionModule.Contracts.Dtos.Requests;
using SelectionModule.Contracts.Dtos.Responses;

namespace SelectionModule.Contracts.Commands.Position;

public record CreatePositionCommand(PositionRequestDto PositionRequestDto) : IRequest<PositionDto>;