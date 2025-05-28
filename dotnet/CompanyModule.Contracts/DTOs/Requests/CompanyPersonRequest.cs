using CompanyModule.Domain.Entities;
using CompanyModule.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.DTOs.Requests;

namespace CompanyModule.Contracts.DTOs.Requests
{
    public class CompanyPersonRequest
    {
        public UserRequest? userRequest { get; set; }
        public Guid? userId { get; set; }
        public string telegram { get; set; }
        public string phone { get; set; }
        public bool isCurator { get; set; }
        

        public CompanyPersonRequest() {}
    }
}
