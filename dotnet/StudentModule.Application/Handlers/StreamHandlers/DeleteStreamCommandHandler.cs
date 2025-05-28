using MediatR;
using Shared.Domain.Exceptions;
using StudentModule.Contracts.DTOs;
using StudentModule.Contracts.Repositories;
using StudentModule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentModule.Contracts.Commands.StreamCommands;

namespace StudentModule.Application.Handlers.StreamHandlers
{
    public class DeleteStreamCommandHandler : IRequestHandler<DeleteStreamCommand, Unit>
    {
        private readonly IStreamRepository _streamRepository;

        public DeleteStreamCommandHandler(IStreamRepository streamRepository)
        {
            _streamRepository = streamRepository;
        }

        public async Task<Unit> Handle(DeleteStreamCommand request, CancellationToken cancellationToken)
        {
            StreamEntity? stream = await _streamRepository.GetByIdAsync(request.StreamId) ?? throw new NotFound("Stream not found");


            await _streamRepository.DeleteAsync(stream);

            return Unit.Value;
        }
    }
}
