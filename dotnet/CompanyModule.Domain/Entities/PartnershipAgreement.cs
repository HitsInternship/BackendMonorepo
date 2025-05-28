using Shared.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Domain.Entities
{
    public class PartnershipAgreement : BaseEntity
    {
        public string Description { get; set; }
        public Guid DocumentId { get; set; }
        public Company Company { get; set; }

        public PartnershipAgreement() {}
    }
}
