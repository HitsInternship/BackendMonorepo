using System.Security.Cryptography;
using AuthModel.Service.Interface;
using AuthModule.Contracts.CQRS;
using AuthModule.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using UserInfrastructure;

namespace AuthModel.Service.Handler;

public class EditPasswordHandler : IRequestHandler<EditPasswordQuery>
{
    private readonly IHashService _hashService;
    private readonly AuthDbContext _context;

    public EditPasswordHandler(IHashService hashService, AuthDbContext context)
    {
        _hashService = hashService;
        _context = context;
    }


    public async Task Handle(EditPasswordQuery request, CancellationToken cancellationToken)
    {
        using SHA256 sha256Hash = SHA256.Create();
        var hash = _hashService.GetHash(sha256Hash, request.OldPassword);

        var aspNetUser = await _context.AspNetUsers.Where(x => x.Id.Equals(request.UserId) && x.Password == hash)
            .FirstOrDefaultAsync(cancellationToken);

        if (aspNetUser == null)
        {
            throw new NotFound("aspNetUser not found");
        }

        var newHash = _hashService.GetHash(sha256Hash, request.NewPassword);
        aspNetUser.Password = newHash;
        await _context.SaveChangesAsync(cancellationToken);
    }
}