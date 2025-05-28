using System.ComponentModel.DataAnnotations;
using UserModule.Domain.Entities;

namespace AuthModule.Domain.Entity;

public class AspNetUser
{
    [Key]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public Guid? UserId { get; set; }
}