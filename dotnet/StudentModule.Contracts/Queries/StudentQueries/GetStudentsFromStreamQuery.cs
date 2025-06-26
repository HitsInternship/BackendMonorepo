using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Queries.StudentQueries
{
    public record GetStudentsFromStreamQuery(Guid UserId, List<string> Roles) : IRequest<List<StudentDto>>
    {
        public Guid streamId { get; set; }
    }
}
