using MediatR;
using PracticeModule.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using CompanyModule.Contracts.Repositories;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using OfficeOpenXml;
using System.Data;
using UserModule.Domain.Entities;
using PracticeModule.Domain.Entity;
using CompanyModule.Domain.Entities;
using SelectionModule.Domain.Entites;
using DeanModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PracticeModule.Application.Handler.Practice
{
    public class GetExelAboutPracticeByCompanyQueryHandler : IRequestHandler<GetExelAboutPracticeByCompanyQuery, FileContentResult>
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GetExelAboutPracticeByCompanyQueryHandler(IPracticeRepository practiceRepository, IUserRepository userRepository, 
            IStudentRepository studentRepository, ICompanyRepository companyRepository,
            IPositionRepository positionRepository, IGroupRepository groupRepository, ISemesterRepository semesterRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<FileContentResult> Handle(GetExelAboutPracticeByCompanyQuery request, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(request.CompanyId);

            SemesterEntity? currentSemester = (await _semesterRepository.ListAllAsync()).Where(semester =>
                semester.EndDate > DateOnly.FromDateTime(DateTime.UtcNow) &&
                semester.StartDate < DateOnly.FromDateTime(DateTime.UtcNow)).FirstOrDefault();

            if (currentSemester == null)
            {
                throw new NotFound("No current semester found");
            }
            var practices = (await _practiceRepository.ListAllAsync()).Where(practice => practice.CompanyId == company.Id && practice.GlobalPractice.SemesterId == currentSemester.Id).ToList();
            var students = (await _studentRepository.ListAllAsync()).Where(student => practices.Select(practice => practice.StudentId).Contains(student.Id)).ToList();
            var users = (await _userRepository.ListAllAsync()).Where(user => students.Select(student => student.UserId).Contains(user.Id)).ToList();

            ExcelPackage.License.SetNonCommercialPersonal("<My Name>");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Практики");

                worksheet.Cells[1, 1].Value = "Фамилия";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Отчество";
                worksheet.Cells[1, 4].Value = "Компания";
                worksheet.Cells[1, 5].Value = "Позиция";

                for (int i = 0; i < practices.Count(); i++)
                {
                    var student = students.First(student => student.Id == practices[i].StudentId);
                    student.User = users.First(user => user.Id == student.UserId);

                    var position = await _positionRepository.GetByIdAsync(practices[i].PositionId);

                    worksheet.Cells[i + 2, 1].Value = student.User.Surname;
                    worksheet.Cells[i + 2, 2].Value = student.User.Name;
                    worksheet.Cells[i + 2, 3].Value = student.Middlename;
                    worksheet.Cells[i + 2, 4].Value = company.Name;
                    worksheet.Cells[i + 2, 5].Value = position.Name;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                byte[] fileContents = package.GetAsByteArray();

                string fileName = $"Практики_{company.Name}.xlsx";
                return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                };

            }    
        }
    }
}
