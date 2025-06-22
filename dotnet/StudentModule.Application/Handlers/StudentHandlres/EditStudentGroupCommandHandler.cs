using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class EditStudentGroupCommandHandler : IRequestHandler<EditStudentGroupCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public EditStudentGroupCommandHandler(IStudentRepository studentRepository, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<StudentDto> Handle(EditStudentGroupCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.studentId) 
                ?? throw new NotFound("Student not found");

            var user = await _userRepository.GetByIdAsync(student.UserId);

            var group = await _groupRepository.GetGroupByIdAsync(request.groupId) 
                ?? throw new NotFound("Group not found");

            

            student.GroupId = request.groupId;
            await _studentRepository.UpdateAsync(student);

            student.User = user;
            student.Group = group;
            return new StudentDto(student);
        }
    }
}
