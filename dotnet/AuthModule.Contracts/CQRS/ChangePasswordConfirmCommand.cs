using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Contracts.CQRS
{
    public record ChangePasswordConfirmCommand : IRequest<Unit>
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
