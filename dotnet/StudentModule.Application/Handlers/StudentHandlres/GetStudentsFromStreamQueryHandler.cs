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
using CompanyModule.Contracts.Queries;
using PracticeModule.Contracts.Queries;
using StudentModule.Domain.Enums;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentsFromStreamQueryHandler : IRequestHandler<GetStudentsFromStreamQuery, List<StudentDto>>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IStreamRepository _streamRepository;

        public GetStudentsFromStreamQueryHandler(IUserRepository userRepository, IStreamRepository streamRepository,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _streamRepository = streamRepository;
            _mediator = mediator;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsFromStreamQuery request,
            CancellationToken cancellationToken)
        {
            var stream = await _streamRepository.GetStreamByIdAsync(request.streamId)
                         ?? throw new NotFound("Stream not found");

            var students = new List<StudentDto>();
            Guid? companyId = null;


            if (request.Roles.Contains("Curator"))
            {
                var curator = await _mediator.Send(new GetCuratorQuery(request.UserId), cancellationToken);
                companyId = curator.Company.Id;
            }

            foreach (var group in stream.Groups)
            {
                foreach (var student in group.Students)
                {
                    var user = await _userRepository.GetByIdAsync(student.UserId);
                    student.User = user;

                    if (request.Roles.Contains("Curator"))
                    {
                        if (student.InternshipStatus == StudentInternshipStatus.Intern)
                        {
                            if (companyId != null)
                            {
                                var practice = await _mediator.Send(new GetStudentPracticeQuery(student.Id),
                                    cancellationToken);

                                if (practice.CompanyId == companyId)
                                {
                                    students.Add(new StudentDto(student));
                                }
                            }
                        }
                    }
                    else
                    {
                        students.Add(new StudentDto(student));
                    }
                }
            }

            return students;
        }
    }
}