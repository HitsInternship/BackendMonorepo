using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Application.Handlers
{
    public class GetUsersByNameQueryHandler : IRequestHandler<GetUsersByNameQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetUsersByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUserByName(request.Name, request.Surname);

            if (users.Count == 0) throw new NotFound("Студент не найден");

            return users;
        }
    }
}
