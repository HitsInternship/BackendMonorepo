using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace AuthModule.Contracts.Model;

public class LoginResponseDTO
{
     public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public List<RoleName> Roles { get; set; }
}