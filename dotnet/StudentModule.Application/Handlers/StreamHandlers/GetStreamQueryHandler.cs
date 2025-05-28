using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StreamQueries;
using StudentModule.Contracts.Repositories;

using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class GetStreamQueryHandler : IRequestHandler<GetStreamQuery, StreamDto>
    {
        private readonly IStreamRepository _streamRepository;

        public GetStreamQueryHandler(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public async Task<StreamDto> Handle(GetStreamQuery request, CancellationToken cancellationToken)
        {
            var stream = await _streamRepository.GetStreamByIdAsync(request.streamId)
                ?? throw new NotFound("Stream not found");

            var groups = new List<GroupDto>();

            foreach (var group in stream.Groups)
            {
                groups.Add(new GroupDto(group));
            }

            var streamDto = new StreamDto(stream) { groups = groups};

            return streamDto;
        }
    }
}
