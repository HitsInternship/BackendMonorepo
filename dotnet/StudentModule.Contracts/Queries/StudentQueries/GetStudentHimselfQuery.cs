using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Queries.StudentQueries
{
    public record GetStudentHimselfQuery : IRequest<StudentDto>
    {
        public Guid userId { get; set; }
    }
}
