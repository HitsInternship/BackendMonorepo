using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StreamQueries;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;


namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class GetStreamsQueryHandler : IRequestHandler<GetStreamsQuery, List<StreamDto>>
    {
        private readonly IStreamRepository _streamRepository;
        private readonly IUserRepository _userRepository;

        public GetStreamsQueryHandler(IStreamRepository streamRepository, IUserRepository userRepository)
        {
            _streamRepository = streamRepository;
            _userRepository = userRepository;
        }

        public async Task<List<StreamDto>> Handle(GetStreamsQuery request, CancellationToken cancellationToken)
        {
            var streams = await _streamRepository.GetStreamsAsync();

            List<StreamDto> streamDtos = new List<StreamDto>();
            List<GroupDto> groupDtos = new List<GroupDto>();

            foreach (var stream in streams)
            {
                groupDtos = new List<GroupDto>();
                foreach (var group in stream.Groups)
                {
                    groupDtos.Add(new GroupDto(group));
                }

                var streamDto = new StreamDto(stream) { groups = groupDtos };
                streamDtos.Add(streamDto);
            }

            return streamDtos;
        }
    }
}
