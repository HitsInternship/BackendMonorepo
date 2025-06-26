using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentModule.Contracts.Commands.StreamCommands;
using StudentModule.Contracts.Queries.StreamQueries;


namespace StudentModule.Controllers.Controllers
{
    [ApiController]
    [Route("api/streams/")]
    public class StreamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StreamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> CreateStream(CreateStreamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("edit")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> EditStream(EditStreamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch]
        [Route("edit-status")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> EditStreamStatus(EditStreamStatusCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> DeleteStream(DeleteStreamCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetStreams()
        {
            var query = new GetStreamsQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("get/{streamId}")]
        public async Task<IActionResult> GetStream([FromRoute] Guid streamId)
        {
            var query = new GetStreamQuery() { streamId = streamId };
            return Ok(await _mediator.Send(query));
        }
    }
}