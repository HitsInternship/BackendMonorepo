using CompanyModule.Contracts.Queries;
using MediatR;
using PracticeModule.Contracts.Queries;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Enums;
using UserModule.Contracts.Repositories;

namespace StudentModule.Application.Handlers.StudentHandlres
{
    public class GetStudentsFromGroupQueryHandler : IRequestHandler<GetStudentsFromGroupQuery, List<StudentDto>>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public GetStudentsFromGroupQueryHandler(IUserRepository userRepository, IGroupRepository groupRepository,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsFromGroupQuery request,
            CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.GroupId)
                        ?? throw new NotFound("Group not found");

            var studentDtos = new List<StudentDto>();

            Guid? companyId = null;


            if (request.Roles.Contains("Curator"))
            {
                var curator = await _mediator.Send(new GetCuratorQuery(request.UserId), cancellationToken);
                companyId = curator.Company.Id;
            }

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
                            var practice = await _mediator.Send(new GetStudentPracticeQuery(student.Id), cancellationToken);

                            if (practice.CompanyId == companyId)
                            {
                                studentDtos.Add(new StudentDto(student));
                            }
                        }
                    }
                }
                else
                {
                    studentDtos.Add(new StudentDto(student));
                }
            }

            return studentDtos;
        }
    }
}