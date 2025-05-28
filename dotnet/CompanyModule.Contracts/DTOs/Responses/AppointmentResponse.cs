using CompanyModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Responses
{
    public class AppointmentResponse
    {
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public List<string> documentIds { get; set; }

        public AppointmentResponse() {}
    }
}
