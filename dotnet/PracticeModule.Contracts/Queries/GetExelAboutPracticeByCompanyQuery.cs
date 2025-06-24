using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PracticeModule.Contracts.Queries
{
    public record GetExelAboutPracticeByCompanyQuery : IRequest<FileContentResult> 
    {
        public Guid CompanyId { get; set; }
        public Guid? SemesterId { get; set; }
    }
}
