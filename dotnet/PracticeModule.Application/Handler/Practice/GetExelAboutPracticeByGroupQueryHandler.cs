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

namespace PracticeModule.Application.Handler.Practice
{
    public class GetExelAboutPracticeByGroupQueryHandler : IRequestHandler<GetExelAboutPracticeByGroupQuery, FileContentResult>
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IGroupRepository _groupRepository;

        public GetExelAboutPracticeByGroupQueryHandler(IPracticeRepository practiceRepository, IUserRepository userRepository, 
            IStudentRepository studentRepository, ICompanyRepository companyRepository,
            IPositionRepository positionRepository, IGroupRepository groupRepository)
        {
            _practiceRepository = practiceRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _companyRepository = companyRepository;
            _positionRepository = positionRepository;
            _groupRepository = groupRepository;
        }

        public async Task<FileContentResult> Handle(GetExelAboutPracticeByGroupQuery request, CancellationToken cancellationToken)
        {
            if (!await _groupRepository.CheckIfExistsAsync(request.GroupId)) 
                throw new NotFound("Группа не найдена");

            var group = await _groupRepository.GetGroupByIdAsync(request.GroupId);
            var students = await _studentRepository.GetStudentsByGroupIdAsync(request.GroupId);

            List<User> users = [];
            List<Guid> studentsId = [];

            foreach (var student in students)
            {
                studentsId.Add(student.Id);
                users.Add(await _userRepository.GetByIdAsync(student.UserId));
            }

            var practices = await _practiceRepository.GetPracticesByStudentIdAsync(studentsId);

            ExcelPackage.License.SetNonCommercialPersonal("<My Name>");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Практики");

                worksheet.Cells[1, 1].Value = "Фамилия";
                worksheet.Cells[1, 2].Value = "Имя";
                worksheet.Cells[1, 3].Value = "Отчество";
                worksheet.Cells[1, 4].Value = "Компания";
                worksheet.Cells[1, 5].Value = "Позиция";

                for (int i = 0; i < practices.Count; i++)
                {
                    var practice = practices[i];

                    var company = await _companyRepository.GetByIdAsync(practice.CompanyId);
                    var position = await _positionRepository.GetByIdAsync(practice.PositionId);

                    var student = students.FirstOrDefault(s => s.Id == practice.StudentId);
                    var user = users.FirstOrDefault(u => u.Id == student?.UserId);

                    if (user != null && student != null)
                    {
                        worksheet.Cells[i + 2, 1].Value = user.Surname;
                        worksheet.Cells[i + 2, 2].Value = user.Name;
                        worksheet.Cells[i + 2, 3].Value = student.Middlename;
                        worksheet.Cells[i + 2, 4].Value = company?.Name ?? "Не указано";
                        worksheet.Cells[i + 2, 5].Value = position?.Name ?? "Не указано";
                    }
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                byte[] fileContents = package.GetAsByteArray();

                string fileName = $"Практики_{group.GroupNumber}.xlsx";
                return new FileContentResult(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = fileName
                };

            }    
        }
    }
}
