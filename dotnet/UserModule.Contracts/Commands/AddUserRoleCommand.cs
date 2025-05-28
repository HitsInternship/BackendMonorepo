using MediatR;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Contracts.Commands
{
    public record AddUserRoleCommand(Guid userId, RoleName roleName) : IRequest<User>;
}