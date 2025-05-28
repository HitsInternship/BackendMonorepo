using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using UserModule.Contracts.Commands;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStreamRepository _streamRepository;
        private readonly IMediator _mediator;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IGroupRepository groupRepository, IMediator mediator, IStreamRepository streamRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
            _streamRepository = streamRepository;
        }
        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {

            User user = null;
            if (request.userRequest != null)
            {
                user = await _mediator.Send(new CreateUserCommand(request.userRequest));
            }
            else if (request.userId == null)
            {
                throw new BadRequest("Provide userRequest or userId");
            }
            user = await _mediator.Send(new AddUserRoleCommand(request.userId ?? user!.Id, RoleName.Student));

            var group = await _groupRepository.GetByIdAsync(request.GroupId)
                ?? throw new NotFound("Group not found");

            StudentEntity student = new StudentEntity()
            {
                UserId = user.Id,
                User = user,
                Middlename = request.Middlename,
                Phone = request.Phone,
                IsHeadMan = request.IsHeadMan,
                Status = request.Status,
                GroupId = request.GroupId,
                Group = group
            };

            await _studentRepository.AddAsync(student);

            var stream = await _streamRepository.GetByIdAsync(group.StreamId);
            student.Group.Stream = stream;

            return new StudentDto(student);
        }
    }
}