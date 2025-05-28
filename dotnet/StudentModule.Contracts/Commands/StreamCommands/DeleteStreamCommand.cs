using MediatR;

namespace StudentModule.Contracts.Commands.StreamCommands
{
    public record DeleteStreamCommand : IRequest<Unit>
    {
        public Guid StreamId { get; set; }
    }
}
