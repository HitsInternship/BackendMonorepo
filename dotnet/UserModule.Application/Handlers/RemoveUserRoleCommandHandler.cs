using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Commands;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Application.Handlers
{
    public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public RemoveUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> Handle(RemoveUserRoleCommand command, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(command.userId);
            if (user == null) throw new NotFound("No user with such id");

            await _roleRepository.GetRolesByUserIdAsync(user.Id);

            Role role = await _roleRepository.GetRoleAsync(command.roleName);
            if (user.Roles.Contains(role)) user.Roles.Remove(role);
            else throw new Conflict("User does not have this role");

            await _userRepository.UpdateAsync(user);

            return user;
        }
    }
}