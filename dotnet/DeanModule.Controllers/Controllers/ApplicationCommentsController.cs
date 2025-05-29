using DeanModule.Contracts.Commands.ApplicationComments;
using DeanModule.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Dtos;
using UserModule.Persistence;

namespace DeanModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления комментариями к заявкам.
/// </summary>
[Authorize]
[ApiController]
[Route("api")]
public class ApplicationCommentsController : ControllerBase
{
    private readonly ISender _mediator;

    public ApplicationCommentsController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать комментарий к заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="comment">Текст комментария.</param>
    [HttpPost, Route("{applicationId}/comments")]
    public async Task<IActionResult> CreateComment(Guid applicationId, [FromBody] string comment)
    {
        return Ok(await _mediator.Send(new AddApplicationCommentCommand(applicationId, comment, User.GetUserId(),
            User.GetRoles())));
    }

    /// <summary>
    /// Удалить комментарий к заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="commentId">Идентификатор комментария.</param>
    [HttpDelete, Route("{applicationId}/comments/{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid applicationId, Guid commentId)
    {
        return Ok(await _mediator.Send(
            new DeleteApplicationCommentCommand(applicationId, commentId, User.GetUserId())));
    }

    /// <summary>
    /// Обновить комментарий к заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="commentId">Идентификатор комментария.</param>
    /// <param name="comment">Новый текст комментария.</param>
    [HttpPut, Route("{applicationId}/comments/{commentId}")]
    public async Task<IActionResult> UpdateComment(Guid applicationId, Guid commentId, [FromBody] string comment)
    {
        return Ok(await _mediator.Send(
            new UpdateApplicationCommentCommand(applicationId, commentId, comment, User.GetUserId())));
    }

    /// <summary>
    /// Получить список комментариев к заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    [HttpGet, Route("{applicationId}/comments")]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplicationComments(Guid applicationId)
    {
        return Ok(await _mediator.Send(new GetApplicationCommentsQuery(applicationId, User.GetUserId())));
    }
}
