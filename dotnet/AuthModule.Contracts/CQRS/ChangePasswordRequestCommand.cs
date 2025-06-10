using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Contracts.CQRS
{
    public record ChangePasswordRequestCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
