using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AuthModule.Contracts.Model;

namespace AuthModule.Contracts.CQRS
{
    public class UploadExcelDTO : IRequest<List<ExcelStudentDTO>>
    {
        [FromForm]
        public IFormFile File { get; set; }
    }
}