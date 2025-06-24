using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PracticeModule.Contracts.Queries
{
    public record GetExelAboutPracticeByGroupQuery : IRequest<FileContentResult> 
    {
        public Guid GroupId { get; set; }
        public Guid? SemesterId { get; set; }
    }
}
