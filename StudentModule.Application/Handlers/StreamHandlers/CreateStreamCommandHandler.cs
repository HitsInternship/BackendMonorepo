using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StreamCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class CreateStreamCommandHandler : IRequestHandler<CreateStreamCommand, StreamDto>
    {
        private readonly IStreamRepository _streamRepository;

        public CreateStreamCommandHandler(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public async Task<StreamDto> Handle(CreateStreamCommand request, CancellationToken cancellationToken)
        {
            if (await _streamRepository.IsStreamWithNumderExistsAsync(request.StreamNumber))
                throw new Conflict($"Stream with number {request.StreamNumber} already exists");

            if (request.Year < 2017 || request.Year > DateTime.Now.Year)
                throw new BadRequest("Invalid year");


            StreamEntity stream = new StreamEntity()
            {
                StreamNumber = request.StreamNumber,
                Year = request.Year,
                Status = request.Status,
                Course = request.Course
            };

            await _streamRepository.AddAsync(stream);

            return new StreamDto(stream);
        }
    }
}
