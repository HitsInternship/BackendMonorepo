using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentsFromGroupQueryHandler : IRequestHandler<GetStudentsFromGroupQuery, List<StudentDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public GetStudentsFromGroupQueryHandler(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsFromGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.GroupId) 
                ?? throw new NotFound("Group not found"); 

            var studentDtos = new List<StudentDto>();

            foreach (var student in group.Students)
            {
                var user = await _userRepository.GetByIdAsync(student.UserId);
                student.User = user;
                studentDtos.Add(new StudentDto(student));
            }

            return studentDtos;
        }
    }
}
