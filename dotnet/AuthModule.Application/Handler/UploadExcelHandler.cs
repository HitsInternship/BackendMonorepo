using AuthModule.Contracts.CQRS;
using AuthModule.Contracts.Model;
using AuthModule.Domain.Entity;
using OfficeOpenXml;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.StudentCommands;
using UserInfrastructure;
using OfficeOpenXml;

namespace AuthModel.Service.Handler;

using MediatR;

public class UploadExcelHandler : IRequestHandler<UploadExcelDTO, List<ExcelStudentDTO>>
{
    private readonly AuthDbContext _context;
    private readonly IMediator _mediator;


    public UploadExcelHandler(AuthDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<List<ExcelStudentDTO>> Handle(UploadExcelDTO request, CancellationToken cancellationToken)
    {
        //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        ExcelPackage.License.SetNonCommercialPersonal("<My Name>");

        if (request.File == null || request.File.Length == 0)
            throw new BadRequest("Файл отсутствует или пустой");

        

        var students = new List<ExcelStudentDTO>();
        var studentEntities = new List<Student>();

        using var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        using var package = new ExcelPackage(stream);

        var worksheet = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
        if (worksheet == null)
            throw new BadRequest("Лист в файле не найден");

        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            var surname = worksheet.Cells[row, 1].Text?.Trim();
            var name = worksheet.Cells[row, 2].Text?.Trim();
            var middleName = worksheet.Cells[row, 3].Text?.Trim();
            var email = worksheet.Cells[row, 4].Text?.Trim();
            var group = worksheet.Cells[row, 5].Text?.Trim();

            if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(group))
                continue;

            students.Add(new ExcelStudentDTO
            {
                Name = name,
                Surname = surname,
                Middlename = middleName,
                Email = email,
                Group = group
            });
        }

        var command = new CreateStudentFromExelCommand
        {
            ExelStudentDto = students
        };

       await _mediator.Send(command, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return students;
    }
}