using DeanModule.Contracts.Commands.StreamSemester;
using DeanModule.Contracts.Dtos.Requests;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeanModule.Controllers.Controllers;

/// <summary>
/// Контроллер для управления связью между потоками и семестрами.
/// </summary>
[ApiController]
[Authorize(Roles = "DeanMember")]
[Route("stream-semester")]
public class StreamSemesterController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Создает связь между потоком и семестром.
    /// </summary>
    /// <param name="streamSemesterRequestDto">Данные для создания связи.</param>
    /// <returns>Результат создания.</returns>
    /// <response code="200">Связь успешно создана.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateStreamSemester([FromBody] StreamSemesterRequestDto streamSemesterRequestDto)
    {
        return Ok(await sender.Send(new CreateStreamSemesterCommand(streamSemesterRequestDto)));
    }

    /// <summary>
    /// Обновляет связь между потоком и семестром.
    /// </summary>
    /// <param name="id">Идентификатор связи.</param>
    /// <param name="streamSemesterRequestDto">Новые данные связи.</param>
    /// <returns>Результат обновления.</returns>
    /// <response code="200">Связь успешно обновлена.</response>
    [HttpPut, Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStreamSemester(Guid id,
        [FromBody] StreamSemesterRequestDto streamSemesterRequestDto)
    {
        return Ok(await sender.Send(new UpdateStreamSemester(id, streamSemesterRequestDto)));
    }

    /// <summary>
    /// Удаляет или архивирует связь между потоком и семестром.
    /// </summary>
    /// <param name="id">Идентификатор связи.</param>
    /// <param name="isArchive">Если <c>true</c>, запись будет архивирована; иначе удалена.</param>
    /// <returns>Результат удаления или архивирования.</returns>
    /// <response code="200">Связь успешно удалена или архивирована.</response>
    [HttpDelete, Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteStreamSemester(Guid id, [FromQuery] bool isArchive = true)
    {
        return Ok(await sender.Send(new DeleteStreamSemesterCommand(id, isArchive)));
    }

    /// <summary>
    /// Получает список семестров, связанных с указанным потоком.
    /// </summary>
    /// <param name="streamId">Идентификатор потока.</param>
    /// <returns>Список связанных семестров.</returns>
    /// <response code="200">Список успешно получен.</response>
    [HttpGet, Route("{streamId:guid}")]
    [ProducesResponseType(typeof(List<StreamSemesterResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStreamSemesters(Guid streamId)
    {
        return Ok(await sender.Send(new GetStreamSemestersByStreamQuery(streamId)));
    }
}