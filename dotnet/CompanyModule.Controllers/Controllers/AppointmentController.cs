using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.DTOs.Requests;
using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Contracts.Queries;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;
using System.ComponentModel.Design;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        /// Добавить временной слот для встречи студентов с компанией.
        /// </summary>
        /// <returns>Созданный слот.</returns>
        [HttpPost]
        [Authorize(Roles = "DeanMember")]
        [Route("timeslots/add")]
        [ProducesResponseType(typeof(TimeslotResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTimeslot(TimeslotRequest createRequest)
        {
            return Ok(_mapper.Map<TimeslotResponse>(await _sender.Send(new AddTimeslotCommand(createRequest))));
        }

        /// <summary>
        /// Удалить временной слот.
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "DeanMember")]
        [Route("timeslots/{timeslotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveTimeslot(Guid timeslotId)
        {
            await _sender.Send(new RemoveTimeslotCommand(timeslotId));

            return Ok();
        }

        /// <summary>
        /// Добавить встречу с компанией.
        /// </summary>
        /// <returns>Встреча с компанией.</returns>
        [HttpPost]
        [Authorize(Roles = "Curator")]
        [Route("{companyId}/appointments/add")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAppointment(Guid companyId, AppointmentRequest createRequest)
        {
            Guid curatorId = new Guid(User.Claims.First().Value);
            Curator curator = await _sender.Send(new GetCuratorQuery(curatorId));

            if (curator.Company.Id != companyId) throw new Forbidden("You dont have access to this company");

            return Ok(_mapper.Map<AppointmentResponse>(await _sender.Send(new AddAppointmentCommand(companyId, createRequest))));
        }

        /// <summary>
        /// Удалить встречу с компанией.
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Curator, DeanMember")]
        [Route("appointments/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAppointment(Guid appointmentId)
        {
            if (User.Claims.Where(claim => claim.Type == ClaimTypes.Role).FirstOrDefault(claim => claim.Value == "DeanMember") != null)
            {
                await _sender.Send(new RemoveAppointmentCommand(appointmentId));
            }
            else
            {
                Guid curatorId = new Guid(User.Claims.First().Value);
                Curator curator = await _sender.Send(new GetCuratorQuery(curatorId));
                Appointment appointment = await _sender.Send(new GetAppointmentQuery(appointmentId));

                if (curator.Company != appointment.Company) throw new Forbidden("You dont have access to this appointment");

                await _sender.Send(new RemoveAppointmentCommand(appointmentId));
            }

            return Ok();
        }

        /// <summary>
        /// Просмотреть встречи с компанией.
        /// </summary>
        /// <returns>Список встреч с компанией.</returns>
        [HttpGet]
        [Authorize(Roles = "Curator, DeanMember")]
        [Route("{companyId}/appointments")]
        [ProducesResponseType(typeof(List<AppointmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointments(Guid companyId)
        {
            Guid curatorId = new Guid(User.Claims.First().Value);
            Curator curator = await _sender.Send(new GetCuratorQuery(curatorId));

            if (curator.Company.Id != companyId) throw new Forbidden("You dont have access to this company");

            return Ok((await _sender.Send(new GetAppointmentsQuery(companyId))).Select(_mapper.Map<AppointmentResponse>));
        }

        /// <summary>
        /// Просмотреть календарь встреч.
        /// </summary>
        /// <returns>Встречи на конкретную неделю.</returns>
        [HttpGet]
        [Authorize(Roles = "Curator, DeanMember")]
        [Route("appointments/calendar")]
        [ProducesResponseType(typeof(Dictionary<DateTime, Dictionary<int, ShortenAppointmentResponse?>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppointmentsCalendar(DateTime startDate, DateTime endDate)
        {
            return Ok((await _sender.Send(new GetTimeslotsQuery(startDate, endDate))).GroupBy(timeslot => timeslot.Date).OrderBy(group => group.Key).ToDictionary(group => group.Key.ToString("dd.MM.yyyy"), group => group.ToDictionary(list => list.PeriodNumber, list => list.Appointment == null ? (object)list.Id : _mapper.Map<ShortenAppointmentResponse>(list.Appointment))));
        }

        /// <summary>
        /// Прикрепить файл к встрече с компанией.
        /// </summary>
        /// <returns>Идентификатор файла.</returns>
        [HttpPost]
        [Authorize(Roles = "Curator")]
        [Route("appointments/{appointmentId}/attachments/add")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAttachment(Guid appointmentId, IFormFile attachment)
        {
            Guid curatorId = new Guid(User.Claims.First().Value);
            Curator curator = await _sender.Send(new GetCuratorQuery(curatorId));
            Appointment appointment = await _sender.Send(new GetAppointmentQuery(appointmentId));

            if (curator.Company != appointment.Company) throw new Forbidden("You dont have access to this appointment");

            return Ok(await _sender.Send(new AddAttachmentCommand(appointmentId, attachment)));
        }


        /// <summary>
        /// Открепить файл от встречи с компанией.
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = "Curator")]
        [Route("attachments/{attachmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAttachment(Guid attachmentId)
        {
            Guid curatorId = new Guid(User.Claims.First().Value);
            Curator curator = await _sender.Send(new GetCuratorQuery(curatorId));
            Attachment attachment = await _sender.Send(new GetAttachmentQuery(attachmentId));

            if (curator.Company != attachment.Appointment.Company) throw new Forbidden("You dont have access to this attachment");

            await _sender.Send(new RemoveAttachmentCommand(attachmentId));

            return Ok();
        }
    }
}
