using MediatR;
using StudentModule.Contracts.DTOs;


namespace StudentModule.Contracts.Queries.StudentQueries
{
    public record GetStudentsFromGroupQuery : IRequest<List<StudentDto>>
    {
        public Guid GroupId { get; set; }
    }
}
