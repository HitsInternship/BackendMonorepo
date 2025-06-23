using MediatR;
using PracticeModule.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using CompanyModule.Contracts.Repositories;
using PracticeModule.Contracts.Repositories;
using SelectionModule.Contracts.Repositories;
using StudentModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using UserModule.Domain.Entities;
using StudentModule.Domain.Entities;
using CompanyModule.Domain.Entities;
using SelectionModule.Domain.Entites;
using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using PracticeModule.Domain.Enum;

namespace PracticeModule.Application.Handler.PracticePart
{
    public class GetExelAboutPracticeByStreamQueryHandler : IRequestHandler<GetExelAboutPracticeByStreamQuery, FileContentResult>
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IStreamRepository _streamRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GetExelAboutPracticeByStreamQueryHandler(IPracticeRepository practiceRepository, IUserRepository userRepository,
            IStudentRepository studentRepository, ICompanyRepository companyRepository,
            IPositionRepository positionRepository, IStreamRepository streamRepository, ISemesterRepository semesterRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _streamRepository = streamRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<FileContentResult> Handle(GetExelAboutPracticeByStreamQuery request, CancellationToken cancellationToken)
        {
            ExcelPackage.License.SetNonCommercialPersonal("<My Name>");

            var stream = await _streamRepository.GetStreamByIdAsync(request.StreamId)
                ?? throw new NotFound("Поток ненайден");

            using var package = new ExcelPackage();

            foreach (var group in stream.Groups)
            {
                var students = await _studentRepository.GetStudentsByGroupIdAsync(group.Id);

                List<User> users = [];
                List<Guid> studentsId = [];

                foreach (var student in students)
                {
                    studentsId.Add(student.Id);
                    users.Add(await _userRepository.GetByIdAsync(student.UserId));
                }

                SemesterEntity? Semester = null;
                try
                {
                    Semester = await _semesterRepository.GetByIdAsync(request.SemesterId);
                }
                catch (InvalidOperationException)
                {
                    throw new NotFound("Semester not found");
                }

                string sem = Semester.StartDate.Month == 9
                           ? $"Осенний семестр {Semester.StartDate.Year}/{Semester.StartDate.Year + 1}"
                           : $"Весенний семестр {Semester.StartDate.Year}";

                var practices = await _practiceRepository.GetPracticesByStudentIdAsync(studentsId, Semester.Id);

                var worksheet = package.Workbook.Worksheets.Add($"Практики гурпа {group.GroupNumber}");

                worksheet.Cells[1, 1].Value = "ФИО";
                worksheet.Cells[1, 2].Value = "Группа";
                worksheet.Cells[1, 3].Value = "Компания";
                worksheet.Cells[1, 4].Value = "Позиция";
                worksheet.Cells[1, 5].Value = "Тип практики";
                worksheet.Cells[1, 6].Value = "Семестр";

                for (int i = 0; i < students.Count(); i++)
                {
                    students[i].User = users.First(user => user.Id == students[i].UserId);
                    string name = students[i].User.Surname + " " + students[i].User.Name + " " + students[i].Middlename;

                    var practice = practices.FirstOrDefault(practice => practice?.StudentId == students[i].Id);
                    Company company = null;
                    PositionEntity position = null;
                    string practiceType = null;

                    if (practice != null)
                    {
                        company = await _companyRepository.GetByIdAsync(practice.CompanyId);
                        position = await _positionRepository.GetByIdAsync(practice.PositionId);
                        practiceType = GetPracticeType(practice.GlobalPractice.PracticeType);
                    }

                    worksheet.Cells[i + 2, 1].Value = name;
                    worksheet.Cells[i + 2, 2].Value = group.GroupNumber;
                    worksheet.Cells[i + 2, 3].Value = company?.Name ?? "-";
                    worksheet.Cells[i + 2, 4].Value = position?.Name ?? "-";
                    worksheet.Cells[i + 2, 5].Value = practiceType;
                    worksheet.Cells[i + 2, 6].Value = sem;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
            }

            byte[] fileContents = package.GetAsByteArray();

            string fileName = $"Практики_{stream.StreamNumber}.xlsx";
            return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };
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
