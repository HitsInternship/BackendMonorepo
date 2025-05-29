using CompanyModule.Contracts.Queries;
using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

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
            Curator? curator = (await _curatorRepository.ListAllAsync()).Where(person => person.UserId == query.personId).FirstOrDefault();
            if (curator == null) throw new NotFound("No company with such user Id");

            curator.User = await _mediator.Send(new GetUserInfoQuery(query.personId));
            
            return curator;
        }
    }
}
