using System.ComponentModel.DataAnnotations;
using DeanModule.Contracts.Commands.Application;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using DeanModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Dtos;
using UserModule.Persistence;

namespace DeanModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления заявками студентов (Applications).
/// Позволяет создавать, обновлять, удалять, просматривать заявки, загружать файлы и шаблоны.
/// </summary>
[ApiController]
[Authorize]
[Route("applications")]
public class ApplicationsController : ControllerBase
{
    private readonly ISender _sender;

    public ApplicationsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Получает список заявок с возможностью фильтрации по статусу, студенту и архивности.
    /// </summary>
    /// <param name="status">Статус заявки для фильтрации (опционально).</param>
    /// <param name="studentId">Идентификатор студента (опционально).</param>
    /// <param name="isArchived">Показывать ли архивные заявки (по умолчанию false).</param>
    /// <param name="page">Номер страницы (по умолчанию 1).</param>
    /// <returns>Список заявок с пагинацией.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApplicationsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplications([FromQuery] ApplicationStatus? status, [FromQuery] Guid? studentId,
        bool isArchived = false, int page = 1)
    {
        return Ok(await _sender.Send(new GetApplicationsQuery(status, studentId, isArchived, page)));
    }

    /// <summary>
    /// Создает новую заявку.
    /// </summary>
    /// <param name="applicationRequestDto">Данные заявки.</param>
    /// <returns>Созданная заявка.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateApplication([FromBody] ApplicationRequestDto applicationRequestDto)
    {
        return Ok(await _sender.Send(new CreateApplicationCommand(applicationRequestDto)));
    }

    /// <summary>
    /// Загружает файл для заявки.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="file">Файл для загрузки.</param>
    /// <returns>Результат загрузки файла.</returns>
    [HttpPost, Route("{applicationId}/file")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromRoute] Guid applicationId, [FromForm] UploadFileRequest file)
    {
        return Ok(await _sender.Send(new UploadApplicationFileCommand(applicationId, file, User.GetUserId())));
    }

    /// <summary>
    /// Обновляет существующую заявку.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="applicationRequestDto">Обновленные данные заявки.</param>
    /// <returns>Обновленная заявка.</returns>
    [HttpPut, Route("{applicationId}")]
    public async Task<IActionResult> UpdateApplication(Guid applicationId,
        [FromForm] ApplicationRequestDto applicationRequestDto)
    {
        return Ok(
            await _sender.Send(new UpdateApplicationCommand(applicationId, applicationRequestDto, User.GetUserId(),
                User.GetRoles())));
    }

    /// <summary>
    /// Удаляет или архивирует заявку.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="isArchive">Архивировать (true) или удалить (false) заявку.</param>
    /// <returns>Результат удаления или архивирования.</returns>
    [HttpDelete, Route("{applicationId}")]
    public async Task<IActionResult> DeleteApplication(Guid applicationId, [FromQuery] bool isArchive = true)
    {
        return Ok(await _sender.Send(new DeleteApplicationCommand(applicationId, isArchive, User.GetUserId(),
            User.GetRoles())));
    }

    /// <summary>
    /// Обновляет статус заявки (например, одобрена, отклонена и т.д.).
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <param name="status">Новый статус заявки (обязателен).</param>
    /// <returns>Результат обновления статуса.</returns>
    [HttpPost, Route("{applicationId}/application-status")]
    [Authorize(Roles = "DeanMember")]
    public async Task<IActionResult> ApproveApplication(Guid applicationId,
        [FromQuery, Required] ApplicationStatus status)
    {
        return Ok(await _sender.Send(new UpdateApplicationStatusCommand(applicationId, status)));
    }

    /// <summary>
    /// Получает полную информацию о конкретной заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <returns>Полная информация о заявке.</returns>
    [HttpGet, Route("{applicationId}")]
    [ProducesResponseType(typeof(ApplicationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplication(Guid applicationId)
    {
        return Ok(await _sender.Send(new GetApplicationQuery(applicationId, User.GetUserId(), User.GetRoles())));
    }

    /// <summary>
    /// Получает файл, прикрепленный к заявке.
    /// </summary>
    /// <param name="applicationId">Идентификатор заявки.</param>
    /// <returns>Файл заявки.</returns>
    [HttpGet, Route("{applicationId}/file")]
    public async Task<IActionResult> GetApplicationFile(Guid applicationId)
    {
        return Ok(await _sender.Send(
            new DownloadApplicationFileCommand(applicationId, User.GetUserId(), User.GetRoles())));
    }

    /// <summary>
    /// Загружает шаблон заявки (только для роли DeanMember).
    /// </summary>
    /// <param name="template">Файл шаблона.</param>
    /// <returns>Результат загрузки шаблона.</returns>
    [HttpPost, Route("template")]
    [Authorize(Roles = "DeanMember")]
    public async Task<IActionResult> UploadTemplate([FromForm] UploadFileRequest template)
    {
        return Ok(await _sender.Send(new UploadApplicationTemplateCommand(template)));
    }

    /// <summary>
    /// Получает шаблон заявки.
    /// </summary>
    /// <returns>Шаблон заявки.</returns>
    [HttpGet, Route("template")]
    public async Task<IActionResult> GetApplicationTemplate()
    {
        return Ok(await _sender.Send(new GetApplicationTemplateCommand()));
    }
}