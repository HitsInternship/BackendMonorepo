using CompanyModule.Domain.Enums;
using Shared.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyStatus Status { get; set; }

        public List<PartnershipAgreement> Agreements { get; set; } = new List<PartnershipAgreement>();
        public List<Curator> Curators { get; set; } = new List<Curator>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Company() { }
    }
}
