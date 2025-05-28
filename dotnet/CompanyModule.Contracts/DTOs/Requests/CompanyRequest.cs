using CompanyModule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Requests
{
    public class CompanyRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public CompanyStatus status { get; set; }

        public CompanyRequest() {}
    }

    public class EditCompanyRequest
    {
        public string name { get; set; }
        public string description { get; set; }

        public EditCompanyRequest() { }
    }
}
