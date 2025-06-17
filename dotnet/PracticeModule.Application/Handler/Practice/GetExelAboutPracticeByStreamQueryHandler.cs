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

namespace PracticeModule.Application.Handler.Practice
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
                SemesterEntity? currentSemester = (await _semesterRepository.ListAllAsync()).Where(semester =>
                    semester.EndDate > DateOnly.FromDateTime(DateTime.UtcNow) &&
                    semester.StartDate < DateOnly.FromDateTime(DateTime.UtcNow)).FirstOrDefault();

                if (currentSemester == null)
                {
                    throw new NotFound("No current semester found");
                }
                var practices = await _practiceRepository.GetPracticesByStudentIdAsync(studentsId, currentSemester.Id);

                var worksheet = package.Workbook.Worksheets.Add($"Практики гурпа {group.GroupNumber}");

                worksheet.Cells[1, 1].Value = "Фамилия";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Отчество";
                worksheet.Cells[1, 4].Value = "Компания";
                worksheet.Cells[1, 5].Value = "Позиция";

                for (int i = 0; i < students.Count(); i ++)
                {
                    students[i].User = users.First(user => user.Id == students[i].UserId);

                    var practice = practices.FirstOrDefault(practice => practice?.StudentId == students[i].Id);
                    Company company = null;
                    PositionEntity position = null;

                    if (practice != null)
                    {
                        company = await _companyRepository.GetByIdAsync(practice.CompanyId);
                        position = await _positionRepository.GetByIdAsync(practice.PositionId);
                    }

                    worksheet.Cells[i + 2, 1].Value = students[i].User.Surname;
                    worksheet.Cells[i + 2, 2].Value = students[i].User.Name;
                    worksheet.Cells[i + 2, 3].Value = students[i].Middlename;
                    worksheet.Cells[i + 2, 4].Value = company?.Name ?? "-";
                    worksheet.Cells[i + 2, 5].Value = position?.Name ?? "-";
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            byte[] fileContents = package.GetAsByteArray();

            string fileName = $"Практики_{stream.StreamNumber}.xlsx";
            return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };
        }
    }
}
