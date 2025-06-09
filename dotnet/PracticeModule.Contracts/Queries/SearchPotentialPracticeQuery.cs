using MediatR;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.Queries
{
    public record SearchPotentialPracticeQuery(SearchPotentialPracticeRequest searchRequest) : IRequest<List<Practice>>;
}
