using DeanModule.Contracts.Commands.Semester;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Dtos;

namespace DeanModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления семестрами.
/// </summary>
[ApiController]
[Authorize(Roles = "DeanMember")]
[Route("semester")]
public class SemesterController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Получает список семестров.
    /// </summary>
    /// <param name="isArchive">Если <c>true</c>, возвращаются архивные семестры; иначе — активные.</param>
    /// <returns>Список семестров.</returns>
    /// <response code="200">Список семестров успешно получен.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<SemesterResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSemesters([FromQuery] bool isArchive = false)
    {
        return Ok(await sender.Send(new GetSemestersQuery(isArchive)));
    }

    /// <summary>
    /// Добавляет новый семестр.
    /// </summary>
    /// <param name="semesterRequestDto">Данные нового семестра.</param>
    /// <returns>Результат создания семестра.</returns>
    /// <response code="200">Семестр успешно создан.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSemester([FromBody] SemesterRequestDto semesterRequestDto)
    {
        return Ok(await sender.Send(new CreateSemesterCommand(semesterRequestDto)));
    }

    /// <summary>
    /// Обновляет существующий семестр.
    /// </summary>
    /// <param name="semesterId">Идентификатор семестра.</param>
    /// <param name="semesterRequestDto">Новые данные семестра.</param>
    /// <returns>Результат обновления.</returns>
    /// <response code="200">Семестр успешно обновлён.</response>
    [HttpPut, Route("{semesterId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSemester(Guid semesterId, [FromBody] SemesterRequestDto semesterRequestDto)
    {
        return Ok(await sender.Send(new UpdateSemesterCommand(semesterId, semesterRequestDto)));
    }

    /// <summary>
    /// Удаляет семестр или перемещает его в архив.
    /// </summary>
    /// <param name="semesterId">Идентификатор семестра.</param>
    /// <param name="isArchive">Если <c>true</c>, семестр архивируется; иначе — удаляется окончательно.</param>
    /// <returns>Результат удаления или архивирования.</returns>
    /// <response code="200">Семестр успешно удалён или архивирован.</response>
    [HttpDelete, Route("{semesterId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSemester(Guid semesterId, [FromQuery] bool isArchive = true)
    {
        return Ok(await sender.Send(new DeleteSemesterCommand(semesterId, isArchive)));
    }
}
