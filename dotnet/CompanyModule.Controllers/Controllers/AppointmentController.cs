using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyModule.Controllers.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/companies/")]
    public class AppointmentController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public AppointmentController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить встречу с компанией.
        /// </summary>
        /// <returns>Встреча с компанией.</returns>
        [HttpPost]
        [Route("{companyId}/appointments/add")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAppointment(Guid companyId, AppointmentRequest createRequest)
        {
            return Ok(_mapper.Map<AppointmentResponse>(await _sender.Send(new AddAppointmentCommand(companyId, createRequest))));
        }

        /// <summary>
        /// Изменить встречу с компанией.
        /// </summary>
        /// <returns>Встреча с компанией.</returns>
        [HttpPut]
        [Route("appointments/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditAppointment(Guid appointmentId, AppointmentRequest editRequest)
        {
            return Ok(_mapper.Map<AppointmentResponse>(await _sender.Send(new EditAppointmentCommand(appointmentId, editRequest))));
        }

        /// <summary>
        /// Просмотреть встречи с компанией.
        /// </summary>
        /// <returns>Список встреч с компанией.</returns>
        [HttpGet]
        [Route("{companyId}/appointments")]
        [ProducesResponseType(typeof(List<AppointmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointments(Guid companyId)
        {
            return Ok((await _sender.Send(new GetAppointmentsQuery(companyId))).Select(_mapper.Map<AppointmentResponse>));
        }

        /// <summary>
        /// Прикрепить файл к встрече с компанией.
        /// </summary>
        /// <returns>Идентификатор файла.</returns>
        [HttpPost]
        [Route("appointments/{appointmentId}/attachments/add")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAttachment(Guid appointmentId, IFormFile attachment)
        {
            return Ok(await _sender.Send(new AddAttachmentCommand(appointmentId, attachment)));
        }


        /// <summary>
        /// Открепить файл от встречи с компанией.
        /// </summary>
        [HttpDelete]
        [Route("attachments/{attachmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAttachment(Guid attachmentId)
        {
            await _sender.Send(new RemoveAttachmentCommand(attachmentId));

            return Ok();
        }
    }
}
