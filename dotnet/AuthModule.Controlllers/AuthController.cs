using AuthModule.Contracts.CQRS;
using AuthModule.Contracts.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;

namespace AuthModule.Controlllers;

/// <summary>
/// Контроллер для аутентификации и управления доступом.
/// </summary>
[ApiController]
[Route("api/auth/")]
public class AuthController: ControllerBase
{

    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Вход в аккаунт.
    /// </summary>
    /// <param name="loginDTO">Данные для входа: логин и пароль.</param>
    /// <returns>Токены доступа: accessToken и refreshToken.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var token = await _mediator.Send(loginDTO);
        return Ok(new { Token = token });
    }

    /// <summary>
    /// Изменение пароля текущего пользователя.
    /// </summary>
    /// <param name="password">Старый и новый пароли, а также логин.</param>
    /// <returns>200 OK при успешной смене пароля.</returns>
    [Authorize]
    [HttpPut("edit/pass")]
    public async Task<IActionResult> Login(EditPasswordDTO password)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null) throw new Unauthorized("UserId - not found");
        
        var newPassword = new EditPasswordQuery()
        {
            OldPassword = password.OldPassword,
            NewPassword = password.NewPassword,
            UserId = Guid.Parse(userId),
            Login = password.Login,
        };
        await _mediator.Send(newPassword);
        return Ok();
    }
    
    
    
    /// <summary>
    /// Получение роли пользователя по ID.
    /// </summary>
    /// <param name="query">Запрос с ID пользователя.</param>
    /// <returns>Роль пользователя.</returns>
    [Authorize]
    [HttpPost("getRoleById")]
    public async Task<IActionResult> GetMyRole(GetRoleQuery query)
    {
        var role = await _mediator.Send(query);
        return Ok(role);
    }
    
    
    /// <summary>
    /// Обновление токена доступа по refresh-токену.
    /// </summary>
    /// <param name="dto">Refresh токен.</param>
    /// <returns>Новые access и refresh токены.</returns>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshDTO dto)
    {
        var tokens = await _mediator.Send(dto);
        return Ok(tokens);
    }

    /// <summary>
    /// Выход пользователя из системы.
    /// </summary>
    /// <returns>Подтверждение выхода.</returns>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst("UserId").Value;

        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID in token" });
        }

        await _mediator.Send(new LogoutDTO { UserId = Guid.Parse(userId) });
        return Ok(new { Message = "Logout successful" });
    }
    
}
