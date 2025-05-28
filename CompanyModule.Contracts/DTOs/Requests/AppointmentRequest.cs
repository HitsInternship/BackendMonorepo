using CompanyModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Requests
{
    public class AppointmentRequest
    {
        public DateTime date { get; set; }
        public string description { get; set; }

        public AppointmentRequest() {}
    }
}
