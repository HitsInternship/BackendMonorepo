using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;
using UserModule.Contracts.Commands;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class CreateStudentFromExelCommandHandler : IRequestHandler<CreateStudentFromExelCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStreamRepository _streamRepository;
        private readonly IMediator _mediator;

        public CreateStudentFromExelCommandHandler(IStudentRepository studentRepository, IGroupRepository groupRepository, IMediator mediator, IStreamRepository streamRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
            _streamRepository = streamRepository;
        }
        public async Task<StudentDto> Handle(CreateStudentFromExelCommand request, CancellationToken cancellationToken)
        {
            UserRequest userRequest = new UserRequest()
            {
                name = request.ExelStudentDto.Name,
                surname = request.ExelStudentDto.Surname,
                email = request.ExelStudentDto.Email
            };

            User user = await _mediator.Send(new CreateUserCommand(userRequest));

            user = await _mediator.Send(new AddUserRoleCommand(user.Id, RoleName.Student));

            var group = await _groupRepository.GetGroupByNumberAsync(int.Parse(request.ExelStudentDto.Group))
                ?? throw new NotFound("Group not found");

            StudentEntity student = new StudentEntity()
            {
                UserId = user.Id,
                User = user,
                Middlename = request.ExelStudentDto.Middleтame,
                Phone = null,
                IsHeadMan = false,
                Status = StudentStatus.InProcess,
                GroupId = group.Id,
                Group = group
            };

            await _studentRepository.AddAsync(student);

            var stream = await _streamRepository.GetByIdAsync(group.StreamId);
            student.Group.Stream = stream;

            return new StudentDto(student);
        }
    }
}
