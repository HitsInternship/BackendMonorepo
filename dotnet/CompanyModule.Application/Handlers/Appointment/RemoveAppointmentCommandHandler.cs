using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;

namespace CompanyModule.Application.Handlers.AppointmentPart
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
            Appointment appointment = await _appointmentRepository.GetByIdAsync(command.appointmentId);

            await _appointmentRepository.DeleteAsync(appointment);

            return Unit.Value;
        }
    }
}
