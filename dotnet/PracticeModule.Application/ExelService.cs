using CompanyModule.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PracticeModule.Contracts.DTOs;
using PracticeModule.Domain.Enum;

namespace PracticeModule.Application
{
    public class ExelService
    {
        public ExelService() { }

        public void FillWorksheetRaw(ExcelWorksheet worksheet, ExelRaw raw, int rawNumber)
        {
            worksheet.Cells[rawNumber, 1].Value = raw.StudentName;
            worksheet.Cells[rawNumber, 2].Value = raw.GroupNumber;
            worksheet.Cells[rawNumber, 3].Value = raw.Company;
            worksheet.Cells[rawNumber, 4].Value = raw.Position;
            worksheet.Cells[rawNumber, 5].Value = GetPracticeType(raw.PracticeType);
            worksheet.Cells[rawNumber, 6].Value = raw.Semestr;
        }

        public void MakeBeautiful(ExcelWorksheet worksheet)
        {
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
        }

        private static string GetPracticeType(GlobalPracticeType type)
        {
            return type switch
            {
                (GlobalPracticeType)1 => "Технологическая",
                _ => "Сость и причмокивать"
            };
        }
    }
}
