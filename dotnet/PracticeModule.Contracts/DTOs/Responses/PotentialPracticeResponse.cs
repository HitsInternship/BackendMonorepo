using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Responses
{
    public class PotentialPracticeResponse
    {
        public Guid studentId {  get; set; }
        public string studentFullName { get; set; }

        public Guid? companyId { get; set; }
        public string companyName { get; set; }
        public Guid? positionId { get; set; }
        public string positionName { get; set; }

        public Guid newCompanyId { get; set; }
        public string newCompanyName { get; set; }
        public Guid newPositionId { get; set; }
        public string newPositionName { get; set; }
    }
}
