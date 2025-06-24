using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Requests
{
    public class SearchPracticeRequest
    {
        public required Guid globalPracticeId { get; set; }
        public Guid? groupId { get; set; }
        public Guid? companyId { get; set; }
        public bool? hasMark { get; set; }
    }
}
