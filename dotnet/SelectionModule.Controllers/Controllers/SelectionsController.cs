using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.Selection;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;
using SelectionModule.Domain.Enums;
using UserModule.Persistence;

namespace SelectionModule.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для работы с процедурами отбора студентов.
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
        /// Создание новой записи отбора для студента.
        /// Только для пользователей с ролью DeanMember.
        /// </summary>
        /// <param name="studentId">ID студента.</param>
        /// <param name="deadline">Крайний срок прохождения отбора.</param>
        [HttpPost]
        [Authorize(Roles = "DeanMember")]
        [Route("{studentId}/selection/add")]
        public async Task<IActionResult> CreateSelection(Guid studentId, [FromBody] DateOnly deadline)
        {
            return Ok(await _sender.Send(new CreateSelectionCommand(studentId, deadline)));
        }

        /// <summary>
        /// Обновление статуса процедуры отбора.
        /// </summary>
        /// <param name="selectionId">ID отбора.</param>
        /// <param name="status">Новый статус.</param>
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
        [HttpGet]
        [Route("{studentId}/selection")]
        [ProducesResponseType(typeof(SelectionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelection(Guid studentId)
        {
            return Ok(await _sender.Send(new GetSelectionQuery(studentId, User.GetUserId(), User.GetRoles())));
        }

        /// <summary>
        /// Получение списка всех отборов с фильтрами.
        /// Только для пользователей с ролью DeanMember.
        /// </summary>
        /// <param name="groupNumber">Номер группы (опционально).</param>
        /// <param name="status">Статус отбора (опционально).</param>
        /// <param name="isArchive">Архивный статус (опционально).</param>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("selections")]
        [ProducesResponseType(typeof(List<ListedSelectionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSelections(int? groupNumber, SelectionStatus? status, bool? isArchive)
        {
            return Ok(await _sender.Send(new GetSelectionsQuery(groupNumber, status, isArchive)));
        }

        /// <summary>
        /// Подтверждение результата отбора.
        /// Доступно для DeanMember, Curator.
        /// </summary>
        /// <param name="selectionId">ID процедуры отбора.</param>
        [HttpPost]
        [Authorize(Roles = "DeanMember, Curator")]
        [Route("selections/{selectionId}/confirm")]
        public async Task<IActionResult> ConfirmSelection(Guid selectionId)
        {
            return Ok(await _sender.Send(new ConfirmSelectionStatusCommand(selectionId)));
        }
    }
}
