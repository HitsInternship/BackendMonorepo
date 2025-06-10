using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.Queries;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentByNameQueryHandler : IRequestHandler<GetStudentByNameQuery, List<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMediator _mediator;
        public GetStudentByNameQueryHandler(IStudentRepository studentRepository, IMediator mediator)
        {
            _studentRepository = studentRepository;
            _mediator = mediator;
        }

        public async Task<List<StudentDto>> Handle(GetStudentByNameQuery request, CancellationToken cancellationToken)
        {
            var userRequest = new GetUsersByNameQuery()
            {
                Name = request.Name,
                Surname = request.Surname
            };

            var users = await _mediator.Send(userRequest);

            var students = new List<StudentDto>();

            foreach (var user in users)
            {
                var student = await _studentRepository.GetStudentByName(user.Id, request.Middlename)
                    ?? throw new NotFound("Студент не найден");
                student.User = user;

                students.Add(new StudentDto(student));
            }

            return students;
        }
    }
}
