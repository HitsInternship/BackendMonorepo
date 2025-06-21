using PracticeModule.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Contracts.DTOs
{
    public class ExelRaw
    {
        public string StudentName { get; set; }
        public GlobalPracticeType PracticeType { get; set; }
        public  string Position { get; set; }
        public string Company { get; set; }
        public string Semestr { get; set; }
        public int GroupNumber { get; set; }
    }
}
