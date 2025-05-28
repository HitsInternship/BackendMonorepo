using CompanyModule.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Queries
{
    public record GetCompanyPersonsQuery(Guid companyId, bool includeCurators, bool includeRepresenters) : IRequest<List<CompanyPerson>>;
}
