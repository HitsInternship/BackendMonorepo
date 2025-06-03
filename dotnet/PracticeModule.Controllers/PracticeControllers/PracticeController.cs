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

    /// <summary>
    /// Добавить характеристику студента
    /// </summary>
    /// <param name="dto">Данные характеристики</param>
    /// <returns>Ничего</returns>
    [HttpPost("student-characteristics")]
    public async Task<IActionResult> StudentCharacteristics([FromForm] StudentCharacteristicsAddQuery dto)
    {
        await _mediator.Send(dto);
        return Ok();
    }

    /// <summary>
    /// Добавить дневник практики студента
    /// </summary>
    /// <param name="dto">Данные дневника практики</param>
    /// <returns>Ничего</returns>
    [HttpPost("student-practice-diary")]
    public async Task<IActionResult> StudentPracticeDiary([FromForm] PracticeDiaryAddQuery dto)
    {
        await _mediator.Send(dto);
        return Ok();
    }
    
    /// <summary>
    /// Получить характеристику студента по ID
    /// </summary>
    /// <param name="id">ID характеристики</param>
    /// <returns>Характеристика студента</returns>
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
    [HttpGet("student-characteristics")]
    public async Task<IActionResult> GetAllStudentCharacteristics()
    {
        var result = await _mediator.Send(new GetAllStudentCharacteristicsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Получить дневник практики по ID
    /// </summary>
    /// <param name="id">ID дневника</param>
    /// <returns>Дневник практики</returns>
    [HttpGet("student-practice-diary/{id}")]
    public async Task<IActionResult> GetPracticeDiary(Guid id)
    {
        var result = await _mediator.Send(new GetPracticeDiaryByIdQuery { Id = id });
        return Ok(result);
    }

    /// <summary>
    /// Получить все дневники практик
    /// </summary>
    /// <returns>Список дневников</returns>
    [HttpGet("student-practice-diary")]
    public async Task<IActionResult> GetAllPracticeDiaries()
    {
        var result = await _mediator.Send(new GetAllPracticeDiariesQuery());
        return Ok(result);
    }
    
    /// <summary>
    /// Добавить комментарий к дневнику практики
    /// </summary>
    /// <param name="diaryId">ID дневника</param>
    /// <param name="request">Данные комментария</param>
    /// <returns>Ничего</returns>
    [HttpPost("practice-diary/{diaryId}/comments")]
    public async Task<IActionResult> AddPracticeDiaryComment(
        Guid diaryId, 
        [FromBody] AddPracticeDiaryCommentRequest request)
    {
        await _mediator.Send(new AddPracticeDiaryCommentCommand 
        { 
            DiaryId = diaryId,
            Comment = request.Comment
        });
        return Ok();
    }

    /// <summary>
    /// Добавить комментарий к характеристике студента
    /// </summary>
    /// <param name="characteristicId">ID характеристики</param>
    /// <param name="request">Данные комментария</param>
    /// <returns>Ничего</returns>
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