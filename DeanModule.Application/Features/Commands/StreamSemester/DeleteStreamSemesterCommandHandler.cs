using DeanModule.Application.Exceptions;
using DeanModule.Contracts.Commands.StreamSemester;
using DeanModule.Contracts.Repositories;
using MediatR;

namespace DeanModule.Application.Features.Commands.StreamSemester;

public class DeleteStreamSemesterCommandHandler : IRequestHandler<DeleteStreamSemesterCommand, Unit>
{
    private readonly IStreamSemesterRepository _streamSemesterRepository;

    public DeleteStreamSemesterCommandHandler(IStreamSemesterRepository streamRepository)
    {
        _streamSemesterRepository = streamRepository;
    }

    public async Task<Unit> Handle(DeleteStreamSemesterCommand request, CancellationToken cancellationToken)
    {
        if (!await _streamSemesterRepository.CheckIfExistsAsync(request.StreamSemesterId))
            throw new StreamSemesterNotFound(request.StreamSemesterId);

        if (request.IsArchive)
            await _streamSemesterRepository.SoftDeleteAsync(request.StreamSemesterId);
        else
            await _streamSemesterRepository.DeleteAsync(
                await _streamSemesterRepository.GetByIdAsync(request.StreamSemesterId));
        
        return Unit.Value;
    }
}