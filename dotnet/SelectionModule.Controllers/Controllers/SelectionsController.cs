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
        /// Получение процедуры отбора для указанного студента.
        /// </summary>
        /// <param name="studentId">ID студента.</param>
        /// <returns>Данные по отбору студента.</returns>
        [HttpGet]
        [Route("{studentId}/selection")]
        [ProducesResponseType(typeof(SelectionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelection(Guid studentId)
        {
            return Ok(await _sender.Send(new GetSelectionQuery(studentId, User.GetUserId(), User.GetRoles())));
        }

        /// <summary>
        /// Получение списка всех процедур отбора с возможностью фильтрации.
        /// Только для пользователей с ролью DeanMember.
        /// </summary>
        /// <param name="semesterId">ID семестра (опционально).</param>
        /// <param name="groupNumber">Номер группы (опционально).</param>
        /// <param name="status">Статус процедуры отбора (опционально).</param>
        /// <returns>Список отборов с учётом применённых фильтров.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("selections")]
        [ProducesResponseType(typeof(List<ListedSelectionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelections(Guid? semesterId, int? groupNumber, SelectionStatus? status)
        {
            return Ok(await _sender.Send(new GetSelectionsQuery(groupNumber, status, semesterId)));
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
        [Route("selection/archive")]
        public async Task<IActionResult> ArchiveSelection()
        {
            return Ok(await _sender.Send(new ArchiveGlobalSelectionCommand()));
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
    }
}
