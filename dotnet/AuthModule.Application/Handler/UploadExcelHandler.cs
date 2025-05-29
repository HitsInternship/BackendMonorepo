using AuthModule.Contracts.CQRS;
using AuthModule.Contracts.Model;
using AuthModule.Domain.Entity;
using OfficeOpenXml;
using Shared.Domain.Exceptions;
using UserInfrastructure;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace AuthModel.Service.Handler;

using MediatR;

public class UploadExcelHandler : IRequestHandler<UploadExcelDTO, List<ExcelStudentDTO>>
{
    private readonly AuthDbContext _context;

    public UploadExcelHandler(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExcelStudentDTO>> Handle(UploadExcelDTO request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
            throw new BadRequest("Файл отсутствует или пустой");

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

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
            var fio = worksheet.Cells[row, 1].Text?.Trim();
            var email = worksheet.Cells[row, 2].Text?.Trim();
            var group = worksheet.Cells[row, 3].Text?.Trim();

            if (string.IsNullOrWhiteSpace(fio) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(group))
                continue;

            students.Add(new ExcelStudentDTO
            {
                //FIO = fio,
                Email = email,
                Group = group
            });

            studentEntities.Add(new Student
            {
                FIO = fio,
                Email = email,
                Group = group
            });
        }
        //todo: обращаться к student module

        await _context.Students.AddRangeAsync(studentEntities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return students;
    }
}