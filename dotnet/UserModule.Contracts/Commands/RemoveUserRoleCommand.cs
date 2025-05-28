using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Contracts.Commands
{
    public record RemoveUserRoleCommand(Guid userId, RoleName roleName) : IRequest<User>;
}