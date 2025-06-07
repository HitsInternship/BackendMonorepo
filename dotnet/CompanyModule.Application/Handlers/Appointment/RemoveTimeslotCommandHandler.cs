using CompanyModule.Contracts.Commands;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;

namespace CompanyModule.Application.Handlers.Appointment
{
    public class RemoveTimeslotCommandHandler : IRequestHandler<RemoveTimeslotCommand, Unit>
    {
        private readonly ITimeslotRepository _timeslotRepository;
        public RemoveTimeslotCommandHandler(ITimeslotRepository timeslotRepository)
        {
            _timeslotRepository = timeslotRepository;
        }

        public async Task<Unit> Handle(RemoveTimeslotCommand command, CancellationToken cancellationToken)
        {
            Timeslot timeslot = await _timeslotRepository.GetByIdAsync(command.timeslotId);

            await _timeslotRepository.DeleteAsync(timeslot);

            return Unit.Value;
        }
    }
}
