using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Enums;

namespace StudentModule.Contracts.Commands.StreamCommands
{
    public record EditStreamStatusCommand() : IRequest<StreamDto>
    {
        public Guid Id { get; set; }
        public StreamStatus Status { get; set; }
    }
}
