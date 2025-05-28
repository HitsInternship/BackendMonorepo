using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class EditAppointmentCommandHandler : IRequestHandler<EditAppointmentCommand, Domain.Entities.Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public EditAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Appointment> Handle(EditAppointmentCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Appointment appointment = await _appointmentRepository.GetByIdAsync(command.appointmentId);

            _mapper.Map(command.editRequest, appointment);

            await _appointmentRepository.UpdateAsync(appointment);

            return appointment;
        }
    }
}
