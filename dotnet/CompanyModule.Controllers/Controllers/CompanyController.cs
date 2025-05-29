using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Queries;
using CompanyModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyModule.Controllers.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/companies/")]
    public class CompanyController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CompanyController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает все компании-партнеры.
        /// </summary>
        /// <returns>Список компаний партнеров.</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CompanyResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies()
        {
            return Ok((await _sender.Send(new GetCompaniesQuery())).Select(_mapper.Map<CompanyResponse>));
        }

        /// <summary>
        /// Добавляет компанию-партнера.
        /// </summary>
        /// <returns>Добавленная компания.</returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCompany(CompanyRequest createRequest)
        {
            return Ok(_mapper.Map<CompanyResponse>(await _sender.Send(new AddCompanyCommand(createRequest))));
        }

        /// <summary>
        /// Изменяет информацию о компании-партнере.
        /// </summary>
        [HttpPut]
        [Route("{companyId}")]
        [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditCompany(Guid companyId, EditCompanyRequest editRequest)
        {
            return Ok(_mapper.Map<CompanyResponse>(await _sender.Send(new EditCompanyCommand(companyId, editRequest))));
        }

        /// <summary>
        /// Добавляет документ о партнерстве с компанией.
        /// </summary>
        /// <returns>Добавленный документ.</returns>
        [HttpPost]
        [Route("{companyId}/agreements/add")]
        [ProducesResponseType(typeof(PartnershipAgreementResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPartnershipAgreement(Guid companyId,
            [FromForm] PartnershipAgreementRequest createRequest)
        {
            return Ok(_mapper.Map<PartnershipAgreementResponse>(
                await _sender.Send(new AddPartnershipAgreementCommand(companyId, createRequest))));
        }

        /// <summary>
        /// Позволяет получить документы о партнерстве c компанией.
        /// </summary>
        /// <returns>Документ о партнерстве.</returns>
        [HttpGet]
        [Route("{companyId}/agreements")]
        [ProducesResponseType(typeof(List<PartnershipAgreementResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPartnershipAgreements(Guid companyId)
        {
            return Ok((await _sender.Send(new GetPartnershipAgreementsQuery(companyId))).Select(
                _mapper.Map<PartnershipAgreementResponse>));
        }

        /// <summary>
        /// Изменяет статус компании-партнера.
        /// </summary>
        [HttpPut]
        [Route("{companyId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeCompanyStatus(Guid companyId, CompanyStatus companyStatus)
        {
            await _sender.Send(new ChangeCompanyStatusCommand(companyId, companyStatus));

            return Ok();
        }

        /// <summary>
        /// Добавить куратора компании.
        /// </summary>
        /// <param name="createRequest.userId">Если необходимо создать куратора на основе уже существующего пользователя.</param>
        /// <returns>Добавленный куратор.</returns>
        [HttpPost]
        [Route("{companyId}/curators/add")]
        [ProducesResponseType(typeof(CuratorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCurator(Guid companyId, CuratorRequest createRequest)
        {
            return Ok(_mapper.Map<CuratorResponse>(
                await _sender.Send(new AddCuratorCommand(companyId, createRequest))));
        }

        /// <summary>
        /// Получить список кураторов.
        /// </summary>
        /// <returns>Список кураторов.</returns>
        [HttpGet]
        [Route("{companyId}/curators")]
        [ProducesResponseType(typeof(List<CuratorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurators(Guid companyId)
        {
            return Ok((await _sender.Send(new GetCuratorsQuery(companyId)))
                .Select(_mapper.Map<CuratorResponse>));
        }

        /// <summary>
        /// Получить информацию о кураторе.
        /// </summary>
        /// <returns>Информация о кураторе.</returns>
        [HttpGet]
        [Authorize(Roles = "Curator")]
        [Route("curator")]
        [ProducesResponseType(typeof(CuratorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCuratorInfo()
        {
            var userId = User.Claims.First().Value.ToString();

            return Ok(_mapper.Map<CuratorResponse>(
                await _sender.Send(new GetCuratorQuery(new Guid(userId)))));
        }
    }
}