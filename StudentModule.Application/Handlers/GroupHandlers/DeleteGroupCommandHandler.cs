using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.Commands.GroupCommands;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;

namespace StudentModule.Application.Handlers.GroupHandlers
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Unit>
    {
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity? group = await _groupRepository.GetByIdAsync(request.GroupId)
                ?? throw new NotFound("Group not found");

            await _groupRepository.DeleteAsync(group);

            return Unit.Value;
        }
    }
}
