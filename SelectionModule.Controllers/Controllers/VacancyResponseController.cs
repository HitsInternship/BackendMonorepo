using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.VacancyResponse;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Domain.Enums;
using UserModule.Persistence;

namespace SelectionModule.Controllers.Controllers;

/// <summary>
/// Контроллер для откликов на вакансии.
/// </summary>
[Authorize]
[Route("api")]
[ApiController]
public class VacancyResponseController : ControllerBase
{
    private readonly ISender _mediator;

    public VacancyResponseController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создание отклика на вакансию от текущего пользователя.
    /// </summary>
    /// <param name="vacancyId">ID вакансии, на которую пользователь откликается.</param>
    [HttpPost]
    [Route("{vacancyId}/respond")]
    public async Task<IActionResult> CreateVacancyResponse([FromRoute] Guid vacancyId)
    {
        return Ok(await _mediator.Send(new CreateVacancyResponseCommand(User.GetUserId(), vacancyId)));
    }

    /// <summary>
    /// Обновление статуса отклика на вакансию.
    /// </summary>
    /// <param name="respondId">ID отклика.</param>
    /// <param name="status">Новый статус отклика.</param>
    [HttpPatch]
    [Route("{respondId}")]
    public async Task<IActionResult> UpdateVacancyResponse([FromRoute] Guid respondId,
        [FromBody] VacancyResponseStatus status)
    {
        return Ok(await _mediator.Send(new ChangeVacancyResponseStatusCommand(respondId, User.GetUserId(), status)));
    }

    /// <summary>
    /// Получение всех откликов на вакансию.
    /// Только для DeanMember, Curator, CompanyRepresenter.
    /// </summary>
    /// <param name="vacancyId">ID вакансии.</param>
    [HttpGet]
    [Route("{vacancyId}/responses")]
    [Authorize(Roles = "DeanMember, Curator, CompanyRepresenter")]
    [ProducesResponseType(typeof(List<VacancyResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVacancyResponse([FromRoute] Guid vacancyId)
    {
        return Ok(await _mediator.Send(new GetVacancyResponsesQuery(vacancyId)));
    }
}