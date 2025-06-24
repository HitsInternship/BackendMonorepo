using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.Queries;

namespace PracticeModule.Controllers.PracticeControllers;

[ApiController]
[Route("api/characteristics/")]
public class CharacteristicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CharacteristicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Добавить характеристику студента
    /// </summary>
    /// <param name="dto">Данные характеристики</param>
    /// <returns>Ничего</returns>
    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> StudentCharacteristics([FromForm] StudentCharacteristicsAddCommand dto)
    {
        await _mediator.Send(dto);
        return Ok();
    }

    /// <summary>
    /// Получить характеристику студента по ID
    /// </summary>
    /// <param name="id">ID характеристики</param>
    /// <returns>Характеристика студента</returns>
    [Authorize]
    [HttpGet("student-characteristics/{id}")]
    public async Task<IActionResult> GetStudentCharacteristic(Guid id)
    {
        var result = await _mediator.Send(new GetStudentCharacteristicByIdQuery { Id = id });
        return Ok(result);
    }

    /// <summary>
    /// Получить все характеристики студентов
    /// </summary>
    /// <returns>Список характеристик</returns>
    [Authorize]
    [HttpGet("student-characteristics")]
    public async Task<IActionResult> GetAllStudentCharacteristics()
    {
        var result = await _mediator.Send(new GetAllStudentCharacteristicsQuery());
        return Ok(result);
    }


    /// <summary>
    /// Добавить комментарий к характеристике студента
    /// </summary>
    /// <param name="characteristicId">ID характеристики</param>
    /// <param name="request">Данные комментария</param>
    /// <returns>Ничего</returns>
    [Authorize]
    [HttpPost("student-characteristic/{characteristicId}/comments")]
    public async Task<IActionResult> AddStudentCharacteristicComment(
        Guid characteristicId, 
        [FromBody] AddStudentCharacteristicCommentRequest request)
    {
        await _mediator.Send(new AddStudentCharacteristicCommentCommand 
        { 
            CharacteristicId = characteristicId,
            Comment = request.Comment
        });
        return Ok();
    }
}