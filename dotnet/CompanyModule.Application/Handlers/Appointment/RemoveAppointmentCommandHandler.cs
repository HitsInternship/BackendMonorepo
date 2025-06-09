using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class RemoveAppointmentCommandHandler : IRequestHandler<RemoveAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public RemoveAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(RemoveAppointmentCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Appointment appointment = await _appointmentRepository.GetByIdAsync(command.appointmentId);

            await _appointmentRepository.DeleteAsync(appointment);

            return Unit.Value;
        }
    }
}
