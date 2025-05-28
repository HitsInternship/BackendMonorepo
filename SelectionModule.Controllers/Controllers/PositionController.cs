using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectionModule.Contracts.Commands.Position;
using SelectionModule.Contracts.Dtos.Requests;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Contracts.Queries;

namespace SelectionModule.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для управления позициями (вакансиями).
    /// Доступ разрешён только пользователям с ролями: DeanMember, Curator, CompanyRepresenter.
    /// </summary>
    [ApiController]
    [Route("api/positions")]
    [Authorize(Roles = "DeanMember, Curator, CompanyRepresenter")]
    public class PositionController : ControllerBase
    {
        private readonly ISender _mediator;

        public PositionController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание новой позиции.
        /// </summary>
        /// <param name="positionRequestDto">Данные новой позиции.</param>
        /// <returns>Созданная позиция.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] PositionRequestDto positionRequestDto)
        {
            return Ok(await _mediator.Send(new CreatePositionCommand(positionRequestDto)));
        }

        /// <summary>
        /// Обновление существующей позиции по ID.
        /// </summary>
        /// <param name="positionId">ID позиции.</param>
        /// <param name="positionRequestDto">Обновлённые данные позиции.</param>
        /// <returns>Обновлённая позиция.</returns>
        [HttpPut]
        [Route("{positionId}")]
        public async Task<IActionResult> UpdatePosition(Guid positionId,
            [FromBody] PositionRequestDto positionRequestDto)
        {
            return Ok(await _mediator.Send(new UpdatePositionCommand(positionId, positionRequestDto)));
        }

        /// <summary>
        /// Удаление позиции по ID.
        /// </summary>
        /// <param name="positionId">ID удаляемой позиции.</param>
        /// <returns>Результат удаления.</returns>
        [HttpDelete]
        [Route("{positionId}")]
        public async Task<IActionResult> DeletePosition(Guid positionId)
        {
            return Ok(await _mediator.Send(new DeletePositionCommand(positionId)));
        }

        /// <summary>
        /// Получение списка всех позиций.
        /// </summary>
        /// <returns>Список позиций.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PositionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPositions()
        {
            return Ok(await _mediator.Send(new GetPositionsQuery()));
        }
    }
}