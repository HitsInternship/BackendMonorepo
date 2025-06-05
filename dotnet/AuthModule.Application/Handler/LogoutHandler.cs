using AuthModule.Contracts.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using UserInfrastructure;

namespace AuthModel.Service.Handler;

public class LogoutHandler : IRequestHandler<LogoutDTO, Unit>
{
    private readonly AuthDbContext _context;

    public LogoutHandler(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(LogoutDTO request, CancellationToken cancellationToken)
    {
        var user = await _context.AspNetUsers
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFound("Пользователь не найден");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.MinValue;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}