using MediatR;
using Microsoft.EntityFrameworkCore;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;
using UserModule.Persistence.Repositories;

namespace UserModule.Application.Handlers
{
    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetListUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetListUserQuery query, CancellationToken cancellationToken)
        {
            return (await _userRepository.ListAllAsync()).Where(user => query.userIds.Contains(user.Id)).ToList();
        }
    }
}
