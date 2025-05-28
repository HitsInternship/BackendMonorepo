using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Domain.Entities;
using CompanyModule.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Commands
{
    public record ChangeCompanyStatusCommand(Guid companyId, CompanyStatus companyStatus) : IRequest<Unit>;
}
