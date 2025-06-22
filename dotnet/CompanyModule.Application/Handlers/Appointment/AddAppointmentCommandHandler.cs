using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationModule.Contracts.Commands;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, Domain.Entities.Appointment>
    {
        private readonly ISender _mediator;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeslotRepository _timeslotRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository,
            ITimeslotRepository timeslotRepository, ICompanyRepository companyRepository, IMapper mapper,
            ISender mediator, ICandidateRepository candidateRepository, IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _timeslotRepository = timeslotRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _mediator = mediator;
            _candidateRepository = candidateRepository;
            _userRepository = userRepository;
        }

        public async Task<Domain.Entities.Appointment> Handle(AddAppointmentCommand command,
            CancellationToken cancellationToken)
        {
            Timeslot timeslot = (await _timeslotRepository.ListAllAsync())
                .Where(timeslot => timeslot.Id == command.createRequest.timeslotId)
                .Include(timeslot => timeslot.Appointment).FirstOrDefault();

            Domain.Entities.Appointment? appointment = timeslot.Appointment;
            if (appointment != null) throw new Conflict("This date and time is already taken");

            var company = await _companyRepository.GetByIdAsync(command.companyId);

            appointment = _mapper.Map<Domain.Entities.Appointment>(command.createRequest);
            appointment.Company = company;
            appointment.Timeslot = timeslot;

            await _appointmentRepository.AddAsync(appointment);

            await SendMeetingMessage(appointment.Id, timeslot.Date, company.Name, cancellationToken);


            return appointment;
        }

        private async Task SendMeetingMessage(Guid appointmentId, DateTime timeslotDate, string companyName,
            CancellationToken cancellationToken)
        {
            var candidates = await (await _candidateRepository.ListAllActiveAsync())
                .Select(candidate => candidate.UserId)
                .ToListAsync(cancellationToken: cancellationToken);

            var emails = await (await _userRepository.ListAllActiveAsync()).Where(user => candidates.Contains(user.Id))
                .Select(user => user.Email)
                .ToListAsync(cancellationToken: cancellationToken);

            await _mediator.Send(new SendMeetingMessageCommand(appointmentId, timeslotDate, companyName, emails),
                cancellationToken);
        }
    }
}