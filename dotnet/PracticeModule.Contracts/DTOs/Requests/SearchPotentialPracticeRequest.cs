using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Requests
{
    public class SearchPotentialPracticeRequest
    {
        public Guid? groupId { get; set; }

        public Guid? oldCompanyId { get; set; }
        public Guid? oldPositionId { get; set; }
    }
}
