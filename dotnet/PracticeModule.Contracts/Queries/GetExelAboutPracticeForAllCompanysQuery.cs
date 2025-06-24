using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.Queries
{
    public record GetExelAboutPracticeForAllCompanysQuery : IRequest<FileContentResult> 
    {
        public Guid SemesterId { get; set; }
    }
}
