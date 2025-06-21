using Microsoft.AspNetCore.Http;
using PracticeModule.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Requests
{
    public class CreateGlobalPracticeRequest
    {
        public GlobalPracticeType practiceType { get; set; }
        public Guid lastSemesterId { get; set; }
        public Guid semesterId { get; set; }
        public Guid streamId { get; set; }

        public IFormFile diaryPatternFile { get; set; }
        public IFormFile characteristicsPatternFile { get; set; }
    }
}
