using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Commands
{
    public record AddPartnershipAgreementCommand(Guid companyId, PartnershipAgreementRequest createRequest) : IRequest<PartnershipAgreement>;
}
