using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.GroupQueries;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.GroupHandlers
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public GetGroupQueryHandler(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.groupId)
                ?? throw new NotFound("Group not found");

            var studentDtos = new List<StudentDto>();

            foreach (var student in group.Students)
            {
                var user = await _userRepository.GetByIdAsync(student.UserId);
                student.User = user;
                studentDtos.Add(new StudentDto(student));
            }

            var groupDto = new GroupDto(group)
            {
                Students = studentDtos
            };

            return groupDto;
        }
    }
}
