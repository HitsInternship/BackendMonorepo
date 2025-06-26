using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.Vacancy;
using SelectionModule.Contracts.Dtos.Requests;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using Shared.Domain.Exceptions;
using UserModule.Persistence;

namespace SelectionModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления вакансиями.
/// </summary>
[Authorize]
[ApiController]
[Route("api/vacancies")]
public class VacancyController : ControllerBase
{
    private readonly ISender _mediator;

    public VacancyController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создание новой вакансии. 
    /// Только пользователи с ролями DeanMember, Curator.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "DeanMember, Curator")]
    public async Task<IActionResult> CreateVacancy([FromBody] VacancyRequestDto vacancyRequestDto)
    {
        if (User.IsInRole("DeanMember"))
        {
            return Ok(await _mediator.Send(new CreateVacancyCommand(vacancyRequestDto)));
        }

        if (User.IsInRole("Curator"))
        {
            return Ok(await _mediator.Send(new CreateVacancyCommand(vacancyRequestDto, User.GetUserId())));
        }

        return Forbid();
    }

    /// <summary>
    /// Обновление вакансии.
    /// </summary>
    [HttpPut]
    [Route("{vacancyId}")]
    [Authorize(Roles = "DeanMember, Curator")]
    public async Task<IActionResult> UpdateVacancy(Guid vacancyId, [FromBody] VacancyRequestDto vacancyRequestDto)
    {
        if (User.IsInRole("DeanMember"))
        {
            return Ok(await _mediator.Send(new UpdateVacancyCommand(vacancyId, vacancyRequestDto)));
        }

        if (User.IsInRole("Curator"))
        {
            return Ok(await _mediator.Send(new UpdateVacancyCommand(vacancyId, vacancyRequestDto, User.GetUserId())));
        }

        return Forbid();
    }

    /// <summary>
    /// Удаление (или архивирование) вакансии.
    /// </summary>
    [HttpDelete]
    [Route("{vacancyId}")]
    [Authorize(Roles = "DeanMember, Curator")]
    public async Task<IActionResult> DeleteVacancy(Guid vacancyId, bool toArchive = true)
    {
        if (User.IsInRole("DeanMember"))
        {
            return Ok(await _mediator.Send(new DeleteVacancyCommand(vacancyId, toArchive)));
        }

        if (User.IsInRole("Curator"))
        {
            return Ok(await _mediator.Send(new DeleteVacancyCommand(vacancyId, toArchive, User.GetUserId())));
        }

        return Forbid();
    }

    /// <summary>
    /// Получение конкретной вакансии по её ID.
    /// </summary>
    [HttpGet]
    [Route("{vacancyId}")]
    [ProducesResponseType(typeof(VacancyDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVacancy(Guid vacancyId)
    {
        return Ok(await _mediator.Send(new GetVacancyQuery(vacancyId, User.GetUserId(), User.GetRoles())));
    }

    /// <summary>
    /// Получение списка вакансий с фильтрацией.
    /// </summary>
    /// <param name="positionId">ID позиции (опционально).</param>
    /// <param name="companyId">ID компании (опционально).</param>
    /// <param name="page">Номер страницы (по умолчанию 1).</param>
    /// <param name="isClosed">Закрытые вакансии (по умолчанию false).</param>
    /// <param name="isArchived">Архивные вакансии (по умолчанию false).</param>
    [HttpGet]
    [ProducesResponseType(typeof(List<VacanciesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllVacancies(Guid? positionId, Guid? companyId, int page = 1,
        bool isClosed = false,
        bool isArchived = false)
    {
        return Ok(await _mediator.Send(new GetVacanciesQuery(User.GetUserId(), User.GetRoles(), isClosed, isArchived,
            page, companyId, positionId)));
    }
}