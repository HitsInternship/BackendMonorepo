using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using MediatR;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Domain.Entities.Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Appointment> Handle(AddAppointmentCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Appointment appointment = _mapper.Map<Domain.Entities.Appointment>(command.createRequest);

            appointment.Company = await _companyRepository.GetByIdAsync(command.companyId);
            await _appointmentRepository.AddAsync(appointment);

            return appointment;
        }
    }
}
