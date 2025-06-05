using AuthModule.Contracts.CQRS;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AuthModule.Controlllers;

[ApiController]
[Route("api/excel")]
public class ExcelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public ExcelController(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    [HttpPost("upload-excel")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadExcel([FromForm] UploadExcelDTO request)
    {
        var result = await _mediator.Send(request);
        return Ok(result); 
    }
    
    [HttpGet("get-excel/example")]
    public IActionResult DownloadExcelExample()
    {
        var currentDir = _env.ContentRootPath;
        
        var dotnetDir = Path.GetFullPath(Path.Combine(currentDir, "..")); 
        
        var filePath = Path.Combine(dotnetDir, "AuthModule.Controlllers", "example.xlsx");

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { Message = "Файл не найден." });
        }

        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, 
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
            "example.xlsx");
    }


}
