using CompanyModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Responses
{
    public class CuratorResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string telegram { get; set; }
        public string phone { get; set; }
    }
}
