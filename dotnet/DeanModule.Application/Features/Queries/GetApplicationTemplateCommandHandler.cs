using DeanModule.Contracts.Queries;
using DocumentModule.Contracts.Queries;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeanModule.Application.Features.Queries;

public class GetApplicationTemplateCommandHandler : IRequestHandler<GetApplicationTemplateCommand, FileContentResult>
{
    private readonly ISender _mediator;

    public GetApplicationTemplateCommandHandler(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<FileContentResult> Handle(GetApplicationTemplateCommand request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetDocumentQuery(Guid.Parse("8bcea8ec-2bc0-4d14-bfd2-e1e151bb8aff"),
            DocumentType.Attachement), cancellationToken);
    }
}