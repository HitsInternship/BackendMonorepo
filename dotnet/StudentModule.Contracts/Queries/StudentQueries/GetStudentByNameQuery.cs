using MediatR;
using StudentModule.Contracts.DTOs;
using StudentModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Queries.StudentQueries
{
    public record GetStudentByNameQuery : IRequest<List<StudentDto>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Middlename { get; set; }
    }
}
