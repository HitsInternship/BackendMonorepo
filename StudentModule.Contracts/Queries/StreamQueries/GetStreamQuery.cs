using MediatR;
using StudentModule.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Contracts.Queries.StreamQueries
{
    public record GetStreamQuery : IRequest<StreamDto>
    {
        public Guid streamId { get; set; }
    }
}
