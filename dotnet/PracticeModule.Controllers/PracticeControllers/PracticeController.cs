using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.DTOs.Responses;
using PracticeModule.Contracts.Queries;

namespace PracticeModule.Controllers.PracticeControllers
{
    [ApiController]
    [Route("api/practice/")]
    public class PracticeController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public PracticeController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить практики студентов с фильтрами.
        /// </summary>
        /// <returns>Практики студентов.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("search")]
        [ProducesResponseType(typeof(List<PracticeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPractice([FromQuery] SearchPracticeRequest searchRequest)
        {
            return Ok((await _sender.Send(new SearchPracticeQuery(searchRequest))).Select(_mapper.Map<PracticeResponse>));
        }

        /// <summary>
        /// Получить потенциальные практики студентов на следующий семестр.
        /// </summary>
        /// <returns>Потенциальные практики студентов.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("potential")]
        [ProducesResponseType(typeof(List<PotentialPracticeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPotentialPractice([FromQuery] SearchPotentialPracticeRequest searchRequest)
        {
            return Ok((await _sender.Send(new SearchPotentialPracticeQuery(searchRequest))).Select(_mapper.Map<PotentialPracticeResponse>));
        }

        /// <summary>
        /// Создать новую глобальную практику.
        /// </summary>
        /// <returns>Глобальная практика.</returns>
        [HttpPost]
        [Authorize(Roles = "DeanMember")]
        [Consumes("multipart/form-data")]
        [Route("global/add")]
        [ProducesResponseType(typeof(GlobalPracticeResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGlobalPractice(CreateGlobalPracticeRequest createRequest)
        {
             return Ok(_mapper.Map<GlobalPracticeResponse>(await _sender.Send(new CreateGlobalPracticeCommand(createRequest))));
        }

        /// <summary>
        /// Поставить оценку за практику.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "DeanMember")]
        [Route("{practiceId}/mark")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkPractices(Guid practiceId, int mark)
        {
            await _sender.Send(new MarkPracticesCommand(practiceId, mark));

            return Ok();
        }

        /// <summary>
        /// Получить exel таблицу с информацией о практиках группы(фио студента, компания, позиция).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("{groupId}/group-practice-exel")]
        public async Task<IActionResult> GetExelForGroup([FromRoute] Guid groupId)
        {
            var querry = new GetExelAboutPracticeByGroupQuery() { GroupId = groupId };

            return Ok(await _sender.Send(querry));
        }

        /// <summary>
        /// Получить exel файл с информацией о практиках потока(по таблице на каждую группу).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("{streamId}/stream-practice-exel")]
        public async Task<IActionResult> GetExelForStream([FromRoute] Guid streamId)
        {
            var querry = new GetExelAboutPracticeByStreamQuery() { StreamId = streamId };

            return Ok(await _sender.Send(querry));
        }
    }
}
