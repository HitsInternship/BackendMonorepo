﻿using DeanModule.Domain.Entities;
using MediatR;
using PracticeModule.Contracts.DTOs.Requests;
using PracticeModule.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeModule.Application.Handler.PracticePart
{
    public record GetGlobalPracticesQuery(Guid? studentUserId = null) : IRequest<List<IGrouping<SemesterEntity, GlobalPractice>>>;
}
