using AutoMapper;
using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class AddTimeslotCommandHandler : IRequestHandler<AddTimeslotCommand, Timeslot>
    {
        private readonly ITimeslotRepository _timeslotRepository;
        private readonly IMapper _mapper;
        public AddTimeslotCommandHandler(ITimeslotRepository timeslotRepository, IMapper mapper)
        {
            _timeslotRepository = timeslotRepository;
            _mapper = mapper;
        }

        public async Task<Timeslot> Handle(AddTimeslotCommand command, CancellationToken cancellationToken)
        {
            Timeslot? timeslot = (await _timeslotRepository.ListAllAsync()).Where(timeslot => timeslot.Date.Date == command.createRequest.date.Date.ToUniversalTime() && timeslot.PeriodNumber == command.createRequest.periodNumber).FirstOrDefault();
            if (timeslot != null) throw new Conflict("This date and time is already scheduled");

            timeslot = _mapper.Map<Timeslot>(command.createRequest);

            await _timeslotRepository.AddAsync(timeslot);

            return timeslot;
        }
    }
}
