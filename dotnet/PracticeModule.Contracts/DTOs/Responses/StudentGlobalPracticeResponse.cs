using PracticeModule.Domain.Entity;
using PracticeModule.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs.Responses
{

    public class StudentGlobalPracticeResponse
    {
        public Guid semesterId { get; set; }
        public DateOnly semesterStartDate { get; set; }
        public DateOnly semesterEndDate { get; set; }
        public GlobalPracticeType practiceType { get; set; }
        public Guid diaryPatternDocumentId { get; set; }
        public Guid characteristicsPatternDocumentId { get; set; }
        public PracticeResponse practice { get; set; }
    }
}
