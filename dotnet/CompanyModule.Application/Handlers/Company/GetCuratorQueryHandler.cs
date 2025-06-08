using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Queries;

namespace CompanyModule.Application.Handlers.Company
{
    public class GetCuratorQueryHandler : IRequestHandler<GetCuratorQuery, Curator>
    {
         private readonly ICuratorRepository _curatorRepository;
        private readonly IMediator _mediator;
        public GetCuratorQueryHandler(ICuratorRepository curatorRepository, IMediator mediator)
        {
            _curatorRepository = curatorRepository;
            _mediator = mediator;
        }

        public async Task<Curator> Handle(GetCuratorQuery query, CancellationToken cancellationToken)
        {
            Curator? curator = (await _curatorRepository.ListAllAsync()).Where(curator => curator.UserId == query.curatorId).Include(curator => curator.Company).FirstOrDefault();
            if (curator == null) throw new NotFound("No curator with such user Id");

            curator.User = await _mediator.Send(new GetUserInfoQuery(query.curatorId));
            
            return curator;
        }
    }
}
