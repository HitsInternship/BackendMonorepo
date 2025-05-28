using AppSettingsModule.Contracts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingsModule.Contracts.Commands
{
    public record CreateSettingsCommand(Guid userId) : IRequest<SettingsDto>;
}
