using System.Security.Cryptography;
using AuthModel.Service.Interface;
using AuthModule.Contracts.CQRS;
using AuthModule.Contracts.Model;
using AuthModule.Domain.Entity;
using MediatR;
using NotificationModule.Contracts.Commands;
using UserInfrastructure;

namespace AuthModel.Service.Handler;

public class CreateAspNetUserHandler : IRequestHandler<CreateAspNetUserQuery, CredInfoDTO>
{
    private readonly ISender _mediator;
    private readonly IHashService _hashService;
    private readonly AuthDbContext _context;

    public CreateAspNetUserHandler(IHashService hashService, AuthDbContext context, IMediator mediator)
    {
        _hashService = hashService;
        _context = context;
        _mediator = mediator;
    }


    public async Task<CredInfoDTO> Handle(CreateAspNetUserQuery request, CancellationToken cancellationToken)
    {
        var genNewAspNetUserDto = new CredInfoDTO()
        {
            UserId = request.UserId,
            Login = request.Email,
            Password = request.Password ?? Guid.NewGuid().ToString(),
        };
        using SHA256 sha256Hash = SHA256.Create();

        _context.AspNetUsers.Add(new AspNetUser()
        {
            Id = Guid.NewGuid(),
            Login = genNewAspNetUserDto.Login,
            Password = _hashService.GetHash(sha256Hash, genNewAspNetUserDto.Password),
            UserId = genNewAspNetUserDto.UserId
        });
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new SendRegistrationMessageCommand(genNewAspNetUserDto.Login, genNewAspNetUserDto.Password),
            cancellationToken);

        return genNewAspNetUserDto;
    }
}