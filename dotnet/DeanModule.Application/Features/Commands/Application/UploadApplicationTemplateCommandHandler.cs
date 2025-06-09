using DeanModule.Contracts.Commands.Application;
using DocumentModule.Contracts.Commands;
using DocumentModule.Domain.Enums;
using MediatR;

namespace DeanModule.Application.Features.Commands.Application;

public class UploadApplicationTemplateCommandHandler : IRequestHandler<UploadApplicationTemplateCommand, Unit>
{
    private readonly ISender _mediator;

    public UploadApplicationTemplateCommandHandler(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UploadApplicationTemplateCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new LoadDocumentCommand(DocumentType.Attachement, request.File.File, "ApplicationTemplate",
                Guid.Parse("8bcea8ec-2bc0-4d14-bfd2-e1e151bb8aff")),
            cancellationToken);

        return Unit.Value;
    }
}