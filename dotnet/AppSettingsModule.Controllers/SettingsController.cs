using AppSettingsModule.Contracts.Commands;
using AppSettingsModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModule.Persistence;

namespace AppSettingsModule.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/settings/")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch]
        [Route("edit-theme")]
        public async Task<IActionResult> EditTheme([FromBody] Theme theme)
        {
            return Ok(await _mediator.Send(new EditThemeCommand(User.GetUserId(), theme)));
        }

        [HttpPatch]
        [Route("edit-language")]
        public async Task<IActionResult> EditLanguage([FromBody] Language language)
        {
            return Ok(await _mediator.Send(new EditLanguageCommand(User.GetUserId(), language)));
        }
    }
}
