using AutoMapper;
using CompanyModule.Contracts.DTOs.Responses;
using DeanModule.Contracts.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeModule.Application.Handler.PracticePart;
using PracticeModule.Contracts.Commands;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Contracts.DTOs.Responses;
using PracticeModule.Contracts.Queries;
using SelectionModule.Contracts.Dtos.Responses;
using StudentModule.Contracts.Queries.StudentQueries;
using StudentModule.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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
        /// Получить глобальные практики.
        /// </summary>
        /// <returns>Список глобальных практик.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("global")]
        [ProducesResponseType(typeof(List<SemesterPracticeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGlobalPractices()
        {
            return Ok((await _sender.Send(new GetGlobalPracticesQuery())).Select(_mapper.Map<SemesterPracticeResponse>));
        }

        /// <summary>
        /// Получить глобальные практики для студента.
        /// </summary>
        /// <returns>Список глобальных практик.</returns>
        [HttpGet]
        [Authorize(Roles = "Student")]
        [Route("global/student")]
        [ProducesResponseType(typeof(List<SemesterPracticeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentGlobalPractices()
        {
            Guid studentUserId = new Guid(User.Claims.First().Value);

            return Ok((await _sender.Send(new GetGlobalPracticesQuery(studentUserId))).Select(_mapper.Map<StudentGlobalPracticeResponse>));
        }

        /// <summary>
        /// Поставить оценку за практику.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "DeanMember")]
        [Route("{practiceId}/mark")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkPractice(Guid practiceId, [Range(2, 5)][Required] int mark)
        {
            await _sender.Send(new MarkPracticeCommand(practiceId, mark));

            return Ok();
        }

        /// <summary>
        /// Получить exel таблицу с информацией о практиках группы(фио студента, компания, позиция).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("{groupId}/group-practice-exel")]
        public async Task<IActionResult> GetExelForGroup([FromRoute] Guid groupId, [FromQuery] Guid? semesterId)
        {
            var querry = new GetExelAboutPracticeByGroupQuery() { GroupId = groupId, SemesterId = semesterId };

            return await _sender.Send(querry);
        }

        /// <summary>
        /// Получить exel файл с информацией о практиках потока(по таблице на каждую группу).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("{streamId}/stream-practice-exel")]
        public async Task<IActionResult> GetExelForStream([FromRoute] Guid streamId, [FromQuery] Guid? semesterId)
        {
            var querry = new GetExelAboutPracticeByStreamQuery() { StreamId = streamId, SemesterId = semesterId };

            return await _sender.Send(querry);
        }

        /// <summary>
        /// Получить exel файл с информацией о практиках по компании.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("{companyId}/company-practice-exel")]
        public async Task<IActionResult> GetExelForCompany([FromRoute] Guid companyId, [FromQuery] Guid? semesterId)
        {
            var querry = new GetExelAboutPracticeByCompanyQuery() { CompanyId = companyId, SemesterId = semesterId };

            return await _sender.Send(querry);
        } 
        
        /// <summary>
        /// Получить exel файл с информацией о практиках для всех компаний.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("company-practice-exel")]
        public async Task<IActionResult> GetExelForAllCompanys([FromQuery] Guid? semesterId)
        {
            var query = new GetExelAboutPracticeForAllCompanysQuery() { SemesterId = semesterId };

            return await _sender.Send(query);
        }

        /// <summary>
        /// Получить статистику по компаниям.
        /// </summary>
        /// <returns>Статистика по компаниям.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("stats/companies")]
        [ProducesResponseType(typeof(Dictionary<SemesterResponseDto, Dictionary<CompanyResponse, int>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPracticeStatisticsByCompanies([FromQuery][Required] List<Guid> companyIds)
        {
            return Ok((await _sender.Send(new GetPracticeStatisticsByCompanyQuery(companyIds))).ToDictionary(keyPair => keyPair.Key.Description, keyPair => keyPair.Value.ToDictionary(keyPair => keyPair.Key.Name, keyPair => keyPair.Value)));
        }

        /// <summary>
        /// Получить статистику по позициям.
        /// </summary>
        /// <returns>Статистика по позициям.</returns>
        [HttpGet]
        [Authorize(Roles = "DeanMember")]
        [Route("stats/positions")]
        [ProducesResponseType(typeof(Dictionary<SemesterResponseDto, Dictionary<PositionDto, int>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPracticeStatisticsByPositions([FromQuery][Required] List<Guid> positionIds, [FromQuery] List<Guid> companyIds)
        {
            return Ok((await _sender.Send(new GetPracticeStatisticsByPositionQuery(positionIds, companyIds))).ToDictionary(keyPair => keyPair.Key.Description, keyPair => keyPair.Value.ToDictionary(keyPair => keyPair.Key.Name, keyPair => keyPair.Value)));
        }
    }
}
