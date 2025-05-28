using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Commands;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Application.Handlers
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public EditUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(EditUserCommand command, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(command.userId);
            if (user == null) throw new NotFound("No user with such id");

            if (user.Email != command.editRequest.email && (await _userRepository.GetByEmailAsync(command.editRequest.email)) != null) throw new Conflict("User with such email already exists");

            await _roleRepository.GetRolesByUserIdAsync(user.Id);

            _mapper.Map(command.editRequest, user);

            await _userRepository.UpdateAsync(user);

            return user;
        }
    }
}