using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationModule.Contracts.Commands;
using Shared.Extensions;

namespace NotificationModule.Controllers;

[ApiController]
[Route("notification")]
//[EnvironmentOnly("Testing")]
public class NotificationController : ControllerBase
{
    private readonly ISender _sender;

    public NotificationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> SendRegisterMessage(SendRegistrationMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("admissions_internship")]
    public async Task<IActionResult> SendAdmissionMessage(SendAdmissionInternshipMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("change_password")]
    public async Task<IActionResult> SendChangePasswordMessage(SendChangePasswordMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("change_practice")]
    public async Task<IActionResult> SendChangePractice(SendChangingPracticeCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("deadline")]
    public async Task<IActionResult> SendDeadlineMessage(SendDeadlineMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("new_comment")]
    public async Task<IActionResult> SendNewCommentMessage(SendNewCommentMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("practice_rate")]
    public async Task<IActionResult> SendPracticeRateMessage(SendRatedForPracticeMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost, Route("appointment")]
    public async Task<IActionResult> SendAppointment(SendMeetingMessageCommand command)
    {
        return Ok(await _sender.Send(command));
    }
}