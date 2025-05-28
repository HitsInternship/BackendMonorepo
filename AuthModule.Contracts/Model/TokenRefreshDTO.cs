using MediatR;

namespace AuthModule.Contracts.Model;

public class TokenRefreshDTO : IRequest<LoginResponseDTO>
{
    public string RefreshToken { get; set; }
}
