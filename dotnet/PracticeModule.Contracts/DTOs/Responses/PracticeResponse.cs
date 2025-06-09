using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Responses
{
    public class PracticeResponse
    {
        public Guid id { get; set; }

        public Guid studentId { get; set; }
        public string studentFullName { get; set; }

        public int? mark { get; set; }

        public Guid practiceDiaryId { get; set; }
        public Guid characteristicsId { get; set; }
    }
}
