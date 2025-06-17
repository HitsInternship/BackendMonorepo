using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PracticeModule.Application.Handler.Practice
{
    public class GetExelAboutPracticeForAllCompanysQueryHandler : IRequestHandler<GetExelAboutPracticeForAllCompanysQuery, FileContentResult>
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IGroupRepository _groupRepository;

        public GetExelAboutPracticeForAllCompanysQueryHandler(IPracticeRepository practiceRepository, IUserRepository userRepository,
            IStudentRepository studentRepository, ICompanyRepository companyRepository,
            IPositionRepository positionRepository, IGroupRepository groupRepository, ISemesterRepository semesterRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _semesterRepository = semesterRepository;
            _groupRepository = groupRepository;
        }

        public async Task<FileContentResult> Handle(GetExelAboutPracticeForAllCompanysQuery request, CancellationToken cancellationToken)
        {
            var companys = await _companyRepository.GetAllAsync();

            SemesterEntity? currentSemester = (await _semesterRepository.ListAllAsync()).Where(semester =>
                semester.EndDate > DateOnly.FromDateTime(DateTime.UtcNow) &&
                semester.StartDate < DateOnly.FromDateTime(DateTime.UtcNow)).FirstOrDefault();

            if (currentSemester == null)
            {
                throw new NotFound("No current semester found");
            }

            ExcelPackage.License.SetNonCommercialPersonal("<My Name>");
            using var package = new ExcelPackage();

            foreach (Company company in companys)
            {
                var practices = (await _practiceRepository.ListAllAsync()).Where(practice => practice.CompanyId == company.Id && practice.GlobalPractice.SemesterId == currentSemester.Id).ToList();
                var students = (await _studentRepository.ListAllAsync()).Where(student => practices.Select(practice => practice.StudentId).Contains(student.Id)).ToList();
                var users = (await _userRepository.ListAllAsync()).Where(user => students.Select(student => student.UserId).Contains(user.Id)).ToList();

                var worksheet = package.Workbook.Worksheets.Add($"{company.Name}");

                worksheet.Cells[1, 1].Value = "Фамилия";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Отчество";
                worksheet.Cells[1, 4].Value = "Группа";
                worksheet.Cells[1, 5].Value = "Компания";
                worksheet.Cells[1, 6].Value = "Позиция";

                for (int i = 0; i < practices.Count(); i++)
                {
                    var student = students.First(student => student.Id == practices[i].StudentId);
                    student.User = users.First(user => user.Id == student.UserId);
                    student.Group = await _groupRepository.GetByIdAsync(student.GroupId);

                    var position = await _positionRepository.GetByIdAsync(practices[i].PositionId);

                    worksheet.Cells[i + 2, 1].Value = student.User.Surname;
                    worksheet.Cells[i + 2, 2].Value = student.User.Name;
                    worksheet.Cells[i + 2, 3].Value = student.Middlename;
                    worksheet.Cells[i + 2, 4].Value = student.Group.GroupNumber;
                    worksheet.Cells[i + 2, 5].Value = company.Name;
                    worksheet.Cells[i + 2, 6].Value = position.Name;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            byte[] fileContents = package.GetAsByteArray();

            string fileName = $"Практики по компаниям.xlsx";
            return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };
        }
    }
}
