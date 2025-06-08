using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Domain.Entities.Appointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeslotRepository _timeslotRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository, ITimeslotRepository timeslotRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _timeslotRepository = timeslotRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.Appointment> Handle(AddAppointmentCommand command, CancellationToken cancellationToken)
        {
            Timeslot timeslot = (await _timeslotRepository.ListAllAsync()).Where(timeslot => timeslot.Id == command.createRequest.timeslotId).Include(timeslot => timeslot.Appointment).FirstOrDefault();

            Domain.Entities.Appointment? appointment = timeslot.Appointment;
            if (appointment != null) throw new Conflict("This date and time is already taken");

            appointment = _mapper.Map<Domain.Entities.Appointment>(command.createRequest);
            appointment.Company = await _companyRepository.GetByIdAsync(command.companyId);
            appointment.Timeslot = timeslot;

            await _appointmentRepository.AddAsync(appointment);

            return appointment;
        }
    }
}
