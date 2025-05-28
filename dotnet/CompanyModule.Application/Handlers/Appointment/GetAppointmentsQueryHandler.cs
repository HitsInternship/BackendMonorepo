using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<Domain.Entities.Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Domain.Entities.Appointment>> Handle(GetAppointmentsQuery query, CancellationToken cancellationToken)
        {
            return (await _appointmentRepository.ListAllAsync()).Where(appointment => appointment.Company.Id == query.companyId).Include(appointment => appointment.Attachments).ToList();
        }
    }
}
