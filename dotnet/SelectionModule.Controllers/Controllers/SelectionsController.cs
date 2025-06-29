using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Dtos.Requests;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Domain.Enums;
using UserModule.Persistence;

namespace SelectionModule.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для работы с процедурами отбора студентов.
    /// Предоставляет операции для создания, редактирования, получения и подтверждения отборов.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api")]
    public class SelectionsController : ControllerBase
    {
        private readonly ISender _sender;

        public SelectionsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Создание новой процедуры глобального отбора студентов.
        /// </summary>
        /// <param name="selectionRequestDto">Данные для создания глобального отбора.</param>
        /// <returns>Результат создания отбора.</returns>
        [HttpPost]
        [Authorize(Roles = "DeanMember")]
        [Route("selection/add")]
        public async Task<IActionResult> CreateSelection([FromBody] SelectionRequestDto selectionRequestDto)
        {
            return Ok(await _sender.Send(new CreateGlobalSelectionCommand(selectionRequestDto)));
        }

        /// <summary>
        /// Обновление статуса процедуры отбора.
        /// </summary>
        /// <param name="selectionId">ID отбора.</param>
        /// <param name="status">Новый статус отбора.</param>
        /// <returns>Результат обновления статуса.</returns>
        [HttpPut]
        [Route("selections/{selectionId}/edit")]
        public async Task<IActionResult> UpdateSelection(Guid selectionId, [FromBody] SelectionStatus status)
        {
            return Ok(await _sender.Send(
                new ChangeSelectionCommand(User.GetUserId(), selectionId, status, User.GetRoles())));
        }

        /// <summary>
        /// Получение текущих глобальных отборов.
        /// </summary>
        /// <param name="isArchived">Архиынее или текущие</param>
        /// <returns></returns>
        [HttpGet, Route("selection/global")]
        [Authorize(Roles = "DeanMember, Curator")]
        [ProducesResponseType(typeof(List<GlobalSelectionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGlobalSelection([FromQuery] bool? isArchived = false)
        {
            return Ok(await _sender.Send(new GetGlobalSelectionsQuery(isArchived ?? false)));
        }

        /// <summary>
        /// Получение отбора по ID.
        /// </summary>
        /// <param name="selectionId"></param>
        /// <param name="semesterId"></param>
        /// <param name="groupNumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet, Route("selection/global/{selectionId}")]
        [Authorize(Roles = "DeanMember, Curator")]
        [ProducesResponseType(typeof(GlobalSelectionResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGlobalSelectionById(Guid selectionId, Guid? semesterId, int? groupNumber,
            SelectionStatus? status)
        {
            return Ok(await _sender.Send(new GetGlobalSelectionQuery(selectionId, groupNumber, status, semesterId,
                User.GetUserId(), User.GetRoles())));
        }


        /// <summary>
        /// Получение процедуры отбора для указанного студента.
        /// </summary>
        /// <param name="selectionId">ID студента.</param>
        /// <returns>Данные по отбору студента.</returns>
        [HttpGet]
        [Route("/selections/{selectionId}")]
        [ProducesResponseType(typeof(SelectionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelection(Guid selectionId)
        {
            return Ok(await _sender.Send(new GetSelectionQuery(selectionId, User.GetUserId(), User.GetRoles(),
                null)));
        }

        /// <summary>
        /// Подтверждение результата процедуры отбора.
        /// Доступно для пользователей с ролями DeanMember и Curator.
        /// </summary>
        /// <param name="selectionId">ID процедуры отбора.</param>
        /// <returns>Результат подтверждения процедуры.</returns>
        [HttpPost]
        [Authorize(Roles = "DeanMember, Curator")]
        [Route("selections/{selectionId}/confirm")]
        public async Task<IActionResult> ConfirmSelection(Guid selectionId)
        {
            return Ok(await _sender.Send(new ConfirmSelectionStatusCommand(selectionId)));
        }

        /// <summary>
        /// Архивирование текущей процедуры глобального отбора.
        /// Доступно только пользователям с ролью DeanMember.
        /// </summary>
        /// <returns>Результат архивирования.</returns>
        [HttpPost]
        [Authorize(Roles = "DeanMember")]
        [Route("selection/global/{id}/archive")]
        public async Task<IActionResult> ArchiveSelection(Guid id)
        {
            return Ok(await _sender.Send(new ArchiveGlobalSelectionCommand(id)));
        }

        /// <summary>
        /// Обновление даты начала новой процедуры глобального отбора.
        /// Доступно только пользователям с ролью DeanMember.
        /// </summary>
        /// <param name="date">Новая дата начала процедуры отбора.</param>
        /// <returns>Результат обновления даты.</returns>
        [HttpPatch]
        [Authorize(Roles = "DeanMember")]
        [Route("selection/edit")]
        public async Task<IActionResult> EditSelection([FromBody] DateOnly date)
        {
            return Ok(await _sender.Send(new UpdateGlobalSelectionCommand(date)));
        }

        /// <summary>
        /// Получить информацию о своем процессе отбора (Selection) для текущего пользователя.
        /// </summary>
        /// <remarks>
        /// Доступно только пользователям с ролью "Student".
        /// Возвращает объект SelectionDto для текущего студента.
        /// </remarks>
        /// <response code="200">Успешно возвращена информация о Selection</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="403">Недостаточно прав (роль отлична от "Student")</response>
        [HttpGet]
        [Authorize(Roles = "Student")]
        [Route("selections/my")]
        [ProducesResponseType(typeof(SelectionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMySelections()
        {
            return Ok(await _sender.Send(new GetMySelectionQuery(User.GetUserId())));
        }
    }
}