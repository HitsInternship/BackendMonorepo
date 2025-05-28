using AppSettingsModule.Contracts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingsModule.Contracts.Queries
{
    public record GetSettingsQuery(Guid userId) : IRequest<SettingsDto>;
}
