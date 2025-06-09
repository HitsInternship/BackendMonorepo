using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.Queries;

namespace PracticeModule.Controllers.PracticeControllers;

[ApiController]
[Route("api/diary/")]
public class DiaryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiaryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Добавить дневник практики студента
    /// </summary>
    /// <param name="dto">Данные дневника практики</param>
    /// <returns>Ничего</returns>
    [HttpPost("student-practice-diary")]
    public async Task<IActionResult> StudentPracticeDiary([FromForm] PracticeDiaryAddCommand dto)
    {
        await _mediator.Send(dto);
        return Ok();
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
}