using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;
using System.Linq;
using UserModule.Contracts.Commands;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class CreateStudentFromExelCommandHandler : IRequestHandler<CreateStudentFromExelCommand, List<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;

        public CreateStudentFromExelCommandHandler(IStudentRepository studentRepository, IGroupRepository groupRepository, IMediator mediator)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
        }
        public async Task<List<StudentDto>> Handle(CreateStudentFromExelCommand request, CancellationToken cancellationToken)
        {
            var response = new List<StudentDto>();

            foreach (var exelStudent in request.ExelStudentDto)
            {
                UserRequest userRequest = new UserRequest()
                {
                    name = exelStudent.Name,
                    surname = exelStudent.Surname,
                    email = exelStudent.Email
                };

                User user = await _mediator.Send(new CreateUserCommand(userRequest));

                user = await _mediator.Send(new AddUserRoleCommand(user.Id, RoleName.Student));

                var group = await _groupRepository.GetGroupByNumberAsync(int.Parse(exelStudent.Group))
                    ?? throw new NotFound("Group not found");

                StudentEntity student = new StudentEntity()
                {
                    UserId = user.Id,
                    User = user,
                    Middlename = exelStudent.Middlename,
                    Phone = null,
                    IsHeadMan = false,
                    Status = StudentStatus.InProcess,
                    InternshipStatus = StudentInternshipStatus.Candidate,
                    GroupId = group.Id,
                    Group = group
                };

                await _studentRepository.AddAsync(student);

                response.Add(new StudentDto(student));
            }

            return response;
        }
    }
}
