using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class GetTimeslotsQueryHandler : IRequestHandler<GetTimeslotsQuery, List<Timeslot>>
    {
        private readonly ITimeslotRepository _timeslotRepository;
        public GetTimeslotsQueryHandler(ITimeslotRepository timeslotRepository)
        {
            _timeslotRepository = timeslotRepository;
        }

        public async Task<List<Timeslot>> Handle(GetTimeslotsQuery query, CancellationToken cancellationToken)
        {
            return (await _timeslotRepository.ListAllAsync()).Where(timeslot => timeslot.Date >= query.startDate.ToUniversalTime() && timeslot.Date <= query.endDate).Include(timeslot => timeslot.Appointment).ThenInclude(appointment => appointment.Company).ToList();
        }
    }
}
