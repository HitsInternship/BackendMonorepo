using CompanyModule.Contracts.DTOs.Responses;
using CompanyModule.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyModule.Contracts.Queries
{
    public record GetTimeslotsQuery(DateTime startDate, DateTime endDate) : IRequest<List<Timeslot>>;
}
