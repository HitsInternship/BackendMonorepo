using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.GroupCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace StudentModule.Application.Handlers.GroupHandlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStreamRepository _streamRepository;

        public CreateGroupCommandHandler(IGroupRepository groupRepository, IStreamRepository streamRepository)
        {
            _groupRepository = groupRepository;
            _streamRepository = streamRepository;
        }
        public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            StreamEntity? stream = await _streamRepository.GetByIdAsync(request.StreamId) 
                ?? throw new NotFound("Stream not found");

            if (await _groupRepository.IsGroupWithNumderExistsAsync(request.GroupNumber))
                throw new Conflict($"Group with number {request.GroupNumber} already exists");

            GroupEntity group = new GroupEntity()
            {
                GroupNumber = request.GroupNumber,
                StreamId = request.StreamId
            };

            await _groupRepository.AddAsync(group);

            return new GroupDto(group);
        }
    }
}
