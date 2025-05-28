using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;

namespace UserModule.Contracts.Queries
{
    public record GetListUserQuery(List<Guid> userIds) : IRequest<List<User>> { }
}
