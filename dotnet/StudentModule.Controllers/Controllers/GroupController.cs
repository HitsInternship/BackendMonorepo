using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentModule.Contracts.Commands.GroupCommands;
using StudentModule.Contracts.Queries.GroupQueries;


namespace StudentModule.Controllers.Controllers
{
    [ApiController]
    [Route("api/groups/")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("edit")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> EditGroup(EditGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeanMember")]
        public async Task<IActionResult> DeleteGroup(DeleteGroupCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetGroups()
        {
            var query = new GetGroupsQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("get/{groupId}")]
        public async Task<IActionResult> GetGroup([FromRoute] Guid groupId)
        {
            var query = new GetGroupQuery() { groupId = groupId };
            return Ok(await _mediator.Send(query));
        }
    }
}
