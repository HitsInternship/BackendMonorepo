using AuthModule.Contracts.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthModule.Controlllers;

[ApiController]
[Route("api/[controller]")]
public class ExcelController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExcelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload-excel")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadExcel([FromForm] UploadExcelDTO request)
    {
        var result = await _mediator.Send(request);
        return Ok(result); 
    }
}
