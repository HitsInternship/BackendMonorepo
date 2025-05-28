using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Queries;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Application.Handlers
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public GetUserInfoQueryHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.userId);
            if (user == null) throw new NotFound("No user with such id");

            await _roleRepository.GetRolesByUserIdAsync(user.Id);

            return user;
        }
    }
}