using DeanModule.Contracts.Commands.DeanMember;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModule.Persistence;

namespace DeanModule.Controllers.Controllers;

/// <summary>
/// Контроллер для работы с сущностью "Декан" (DeanMember).
/// Содержит методы для создания и получения данных о деканах.
/// </summary>
[Authorize] // Требует аутентификации для всех методов контроллера
[ApiController]
[Route("api/dean_member")]
public class DeanMemberController : ControllerBase
{
    private readonly ISender _mediator;

    public DeanMemberController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создаёт нового сотрудника деканата. Если передан userId, то пользователь получит роль сотрудника деканата
    /// </summary>
    /// <param name="dto">Данные сотрудника деканата</param>
    /// <param name="userId"></param>
    [HttpPost]
    [Authorize(Roles = "DeanMember")]
    public async Task<IActionResult> CreateDeanMember([FromBody] DeanMemberRequestDto dto, [FromQuery] Guid? userId)
    {
        return Ok(await _mediator.Send(new CreateDeanMemberCommand(dto, userId)));
    }

    /// <summary>
    /// Получает информацию о декане.
    /// Если Guid userId не передан, возвращаются данные текущего пользователя.
    /// </summary>
    /// <param name="userId">Необязательный идентификатор пользователя. (если не указан, то Id берется из токена)</param>
    [HttpGet]
    [ProducesResponseType(typeof(DeanMemberDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeanMembers([FromQuery] Guid? userId)
    {
        return Ok(await _mediator.Send(new GetDeanMemberQuery(userId ?? User.GetUserId())));
    }
}