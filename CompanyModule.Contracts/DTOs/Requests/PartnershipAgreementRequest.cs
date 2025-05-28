using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Requests
{
    public class PartnershipAgreementRequest
    {
        public string description { get; set; }
        public IFormFile file { get; set; }

        public PartnershipAgreementRequest() {}
    }
}
