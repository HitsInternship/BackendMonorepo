using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentsFromStreamQueryHandler : IRequestHandler<GetStudentsFromStreamQuery, List<StudentDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStreamRepository _streamRepository;

        public GetStudentsFromStreamQueryHandler(IUserRepository userRepository, IStreamRepository streamRepository)
        {
            _userRepository = userRepository;
            _streamRepository = streamRepository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsFromStreamQuery request, CancellationToken cancellationToken)
        {
            var stream = await _streamRepository.GetStreamByIdAsync(request.streamId) 
                ?? throw new NotFound("Stream not found");

            var students = new List<StudentDto>();

            foreach (var group in stream.Groups)
            {
                foreach (var student in group.Students)
                {
                    var user = await _userRepository.GetByIdAsync(student.UserId);
                    student.User = user;
                    students.Add(new StudentDto(student));
                }
            }

            return students;
        }
    }
}
