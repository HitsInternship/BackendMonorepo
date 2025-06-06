using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyModule.Application.Handlers.AppointmentPart
{
    public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> Handle(GetAppointmentQuery query, CancellationToken cancellationToken)
        {
            return (await _appointmentRepository.ListAllAsync()).Where(appointment => appointment.Id == query.appointmentId).Include(appointment => appointment.Company).FirstOrDefault();
        }
    }
}
