using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, Domain.Entities.Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Domain.Entities.Appointment> Handle(GetAppointmentQuery query, CancellationToken cancellationToken)
        {
            return (await _appointmentRepository.ListAllAsync()).Where(appointment => appointment.Id == query.appointmentId).Include(appointment => appointment.Company).FirstOrDefault();
        }
    }
}
