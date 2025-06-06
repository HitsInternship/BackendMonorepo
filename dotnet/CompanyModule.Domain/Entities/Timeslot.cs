using Shared.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Domain.Entities
{
    public class Timeslot : BaseEntity
    {
        public int PeriodNumber { get; set; }
        public DateTime Date { get; set; }

        public Appointment? Appointment { get; set; }

        public Timeslot(){}
    }
}
