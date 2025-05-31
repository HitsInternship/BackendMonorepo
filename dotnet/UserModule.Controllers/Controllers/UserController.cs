using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserModule.Contracts.Commands;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Contracts.DTOs.Responses;
using UserModule.Contracts.Queries;
using UserModule.Persistence;


namespace UserModule.Controllers.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListUserInfo([FromQuery] SearchUserRequest searchRequest)
        {
            return Ok((await _mediator.Send(new GetListSearchUserQuery(searchRequest))).Select(_mapper.Map<UserResponse>));
        }

        /// <summary>
        /// Возвращает информацию о пользователе.
        /// </summary>
        /// <returns>Информация о пользователе.</returns>
        [HttpGet]
        [Route("/info")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInfo([FromQuery] Guid? id)
        {
            return Ok(_mapper.Map<UserResponse>(await _mediator.Send(new GetUserInfoQuery(id ?? User.GetUserId()))));
        }

        /// <summary>
        /// Изменяет информацию о пользователе.
        /// </summary>
        /// <returns>Измененный пользователь.</returns>
        [HttpPost]
        [Route("{id}/edit")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditUser(Guid id, UserRequest userRequest)
        {
            return Ok(_mapper.Map<UserResponse>(await _mediator.Send(new EditUserCommand(id, userRequest))));
        }
    }
}
