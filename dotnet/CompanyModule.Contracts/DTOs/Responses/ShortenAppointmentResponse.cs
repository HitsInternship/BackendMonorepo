using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Responses
{
    public class ShortenAppointmentResponse
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public string companyName { get; set; }
    }
}
