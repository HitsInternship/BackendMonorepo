using CompanyModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.DTOs.Requests
{
    public class TimeslotRequest
    {
        public DateTime date { get; set; }
        public int periodNumber { get; set; }

        public TimeslotRequest() {}
    }
}
