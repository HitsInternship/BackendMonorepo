using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PracticeModule.Contracts.DTOs;
using PracticeModule.Contracts.Queries;
using PracticeModule.Contracts.Repositories;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

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
        private readonly ExelService _exelService;

        public GetExelAboutPracticeForAllCompanysQueryHandler(IPracticeRepository practiceRepository, IUserRepository userRepository,
            IStudentRepository studentRepository, ICompanyRepository companyRepository,
            IPositionRepository positionRepository, IGroupRepository groupRepository,
            ISemesterRepository semesterRepository, ExelService exelService)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _semesterRepository = semesterRepository;
            _groupRepository = groupRepository;
            _exelService = exelService;
        }

        public async Task<FileContentResult> Handle(GetExelAboutPracticeForAllCompanysQuery request, CancellationToken cancellationToken)
        {
            var companys = await _companyRepository.GetAllAsync();
            SemesterEntity? Semester = null;

            if (request.SemesterId == null)
            {
                Semester = (await _semesterRepository.ListAllAsync()).Where(semester =>
                    semester.EndDate > DateOnly.FromDateTime(DateTime.UtcNow) &&
                    semester.StartDate < DateOnly.FromDateTime(DateTime.UtcNow)).FirstOrDefault();
            }
            else
            {
                try
                {
                    Semester = await _semesterRepository.GetByIdAsync((Guid)request.SemesterId);
                }
                catch (InvalidOperationException)
                {
                    throw new NotFound("Semester not found");
                }
            }

            ExcelPackage.License.SetNonCommercialPersonal("<My Name>");
            using var package = new ExcelPackage();

            foreach (Company company in companys)
            {
                var practices = (await _practiceRepository.ListAllAsync())
                    .Include(p => p.GlobalPractice)
                    .Where(practice => practice.CompanyId == company.Id &&
                    practice.GlobalPractice.SemesterId == Semester.Id)
                    .ToList();

                var students = (await _studentRepository.ListAllAsync())
                    .Include(s => s.Group)
                    .Where(student => practices.Select(practice => practice.StudentId)
                    .Contains(student.Id))
                    .ToList();

                var users = (await _userRepository.ListAllAsync())
                    .Where(user => students.Select(student => student.UserId)
                    .Contains(user.Id))
                    .ToList();

                string sem = Semester.StartDate.Month == 9
                        ? $"Осенний семестр {Semester.StartDate.Year}/{Semester.StartDate.Year + 1}"
                        : $"Весенний семестр {Semester.StartDate.Year}";

                var worksheet = package.Workbook.Worksheets.Add($"{company.Name}");

                worksheet.Cells[1, 1].Value = "ФИО";
                worksheet.Cells[1, 2].Value = "Группа";
                worksheet.Cells[1, 3].Value = "Компания";
                worksheet.Cells[1, 4].Value = "Позиция";
                worksheet.Cells[1, 5].Value = "Тип практики";
                worksheet.Cells[1, 6].Value = "Семестр";


                for (int i = 0; i < students.Count(); i++)
                {
                    var student = students[i];
                    student.User = users.First(user => user.Id == student.UserId);
                    var practice = practices.First(practice => practice.StudentId == student.Id);

                    var position = await _positionRepository.GetByIdAsync(practices[i].PositionId);


                    ExelRaw exelRaw = new()
                    {
                        StudentName = student.User.Surname + " " + student.User.Name + " " + student.Middlename,
                        PracticeType = practices[i].GlobalPractice.PracticeType,
                        Position = position.Name,
                        Company = company.Name,
                        Semestr = sem,
                        GroupNumber = student.Group.GroupNumber
                    };

                    _exelService.FillWorksheetRaw(worksheet, exelRaw, i + 2);
                }

                _exelService.MakeBeautiful(worksheet);
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
