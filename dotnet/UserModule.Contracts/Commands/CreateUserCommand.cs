﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Contracts.DTOs.Requests;
using UserModule.Domain.Entities;

namespace UserModule.Contracts.Commands
{
    public record CreateUserCommand(UserRequest CreateRequest, string? Password = null) : IRequest<User>;
}