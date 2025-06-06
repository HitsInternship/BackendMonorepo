using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.AppointmentPart
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsQuery query, CancellationToken cancellationToken)
        {
            return (await _appointmentRepository.ListAllAsync()).Where(appointment => appointment.Company.Id == query.companyId).Include(appointment => appointment.Timeslot).Include(appointment => appointment.Attachments).ToList();
        }
    }
}
