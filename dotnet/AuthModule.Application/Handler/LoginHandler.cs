using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthModel.Service.Interface;
using AuthModel.Service.Service;
using AuthModule.Contracts.CQRS;
using AuthModule.Contracts.Model;
using AuthModule.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;
using UserInfrastructure;
using UserModule.Domain.Enums;


namespace AuthModel.Service.Handler;

public class LoginHandler : IRequestHandler<LoginDTO, LoginResponseDTO>
{
    private readonly IHashService _hashService;
    private readonly AuthDbContext _context;
    private readonly IRoleRepository _roleRepository;
    
    public LoginHandler(IHashService hashService, AuthDbContext context, IRoleRepository roleRepository)
    {
        _hashService = hashService;
        _context = context;
        _roleRepository = roleRepository;
    }
    
    public async Task<LoginResponseDTO> Handle(LoginDTO request, CancellationToken cancellationToken)
    {
        using SHA256 sha256Hash = SHA256.Create();
        var passwordHash = _hashService.GetHash(sha256Hash, request.Password);

        var user = await _context.AspNetUsers
            .FirstOrDefaultAsync(u => u.Login == request.Login && u.Password == passwordHash, cancellationToken);

        if (user == null)
        {
            throw new NotFound("Пользователь не найден");
        }

        var accessToken = await GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _roleRepository.GetRolesByUserIdAsync(user.UserId.Value);
        var roleNames = new List<RoleName>(roles.Count);
        foreach (var role in roles)
        {
            roleNames.Add(role.RoleName);
        }

        return new LoginResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Roles = roleNames
        };
    }
    
    private async Task<string> GenerateAccessToken(AspNetUser user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.PrivateKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> { new Claim("UserId", user.UserId.ToString()) };


        var roles = await _roleRepository.GetRolesByUserIdAsync(user.UserId.Value);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleName.ToString()));

            role.Users = null;
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = AuthSettings.GetTokenExpires(),
            SigningCredentials = credentials
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}