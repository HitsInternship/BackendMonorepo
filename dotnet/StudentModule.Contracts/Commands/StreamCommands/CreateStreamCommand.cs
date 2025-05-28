using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Enums;

namespace StudentModule.Contracts.Commands.StreamCommands
{
    public record CreateStreamCommand() : IRequest<StreamDto>
    {
        public int StreamNumber { get; set; }
        public int Year { get; set; }
        public StreamStatus Status { get; set; }
        public int Course { get; set; }
    }
}
