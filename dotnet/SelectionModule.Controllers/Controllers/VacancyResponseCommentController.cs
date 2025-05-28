using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.VacancyResponseComment;
using SelectionModule.Contracts.Queries;
using Shared.Contracts.Dtos;
using UserModule.Persistence;

namespace SelectionModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления комментариями к откликам на вакансии.
/// </summary>
[Authorize]
[ApiController]
[Route("api/vacancies/responses")]
public class VacancyResponseCommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public VacancyResponseCommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создаёт комментарий к отклику на вакансию.
    /// </summary>
    /// <param name="responseId">Идентификатор отклика.</param>
    /// <param name="comment">Текст комментария.</param>
    [HttpPost, Route("{responseId}")]
    public async Task<IActionResult> CreateResponseComment(Guid responseId, [FromBody] string comment)
    {
        return Ok(await _mediator.Send(
            new CreateVacancyResponseCommentCommand(responseId, User.GetUserId(), comment, User.GetRoles())));
    }

    /// <summary>
    /// Получает список комментариев к отклику на вакансию.
    /// </summary>
    /// <param name="responseId">Идентификатор отклика.</param>
    [HttpGet, Route("{responseId}")]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetResponseComment(Guid responseId)
    {
        return Ok(await _mediator.Send(
            new GetVacancyResponseCommentsQuery(responseId, User.GetUserId(), User.GetRoles())));
    }

    /// <summary>
    /// Обновляет комментарий к отклику на вакансию.
    /// </summary>
    /// <param name="responseId">Идентификатор отклика.</param>
    /// <param name="commentId">Идентификатор комментария.</param>
    /// <param name="comment">Новый текст комментария.</param>
    [HttpPut, Route("{responseId}/comments/{commentId}")]
    public async Task<IActionResult> UpdateResponseComment(Guid responseId, Guid commentId, [FromBody] string comment)
    {
        return Ok(await _mediator.Send(
            new UpdateVacancyResponseCommentCommand(responseId, commentId, User.GetUserId(), comment)));
    }

    /// <summary>
    /// Удаляет комментарий к отклику на вакансию.
    /// </summary>
    /// <param name="responseId">Идентификатор отклика.</param>
    /// <param name="commentId">Идентификатор комментария.</param>
    [HttpDelete, Route("{responseId}/comments/{commentId}")]
    public async Task<IActionResult> DeleteResponseComment(Guid responseId, Guid commentId)
    {
        return Ok(
            await _mediator.Send(new DeleteVacancyResponseCommentCommand(responseId, commentId, User.GetUserId())));
    }
}