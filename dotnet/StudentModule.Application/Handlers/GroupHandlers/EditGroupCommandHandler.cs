using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.GroupCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.GroupHandlers
{
    public class EditGroupCommandHandler : IRequestHandler<EditGroupCommand, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStreamRepository _streamRepository;

        public EditGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository, IStreamRepository streamRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _streamRepository = streamRepository;
        }
        public async Task<GroupDto> Handle(EditGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity? group = await _groupRepository.GetGroupByIdAsync(request.Id)
                ?? throw new NotFound("Stream not found");

            if (await _groupRepository.IsGroupWithNumderExistsAsync(request.GroupNumber))
                throw new Conflict($"Group with number {request.GroupNumber} already exists");

            group.GroupNumber = request.GroupNumber;

            await _groupRepository.UpdateAsync(group);

            return await GetGroup(group);
        }

        private async Task<GroupDto> GetGroup(GroupEntity group)
        {
            var studentDtos = new List<StudentDto>();

            foreach (var student in group.Students)
            {
                var user = await _userRepository.GetByIdAsync(student.UserId);
                student.User = user;
                studentDtos.Add(new StudentDto(student));
            }

            var groupDto = new GroupDto(group);
            groupDto.Students = studentDtos;

            return groupDto;
        }
    }
}
