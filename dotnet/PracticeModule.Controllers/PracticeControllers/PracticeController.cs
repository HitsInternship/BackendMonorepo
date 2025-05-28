using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Contracts.CQRS;
using PracticeModule.Contracts.Model;

namespace PracticeModule.Controllers.PracticeControllers;

[ApiController]
[Route("api/practice/")]
public class PracticeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PracticeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("student-characteristics")]
    public async Task<IActionResult> StudentCharacteristics([FromForm] StudentCharacteristicsAddQuery dto)
    {
        await _mediator.Send(dto);
        return Ok();
    }

    [HttpPost("student-practice-diary")]
    public async Task<IActionResult> GetPractice([FromForm] PracticeDiaryAddQuery dto)
    {
        await _mediator.Send(dto);
        return Ok();
    }
}