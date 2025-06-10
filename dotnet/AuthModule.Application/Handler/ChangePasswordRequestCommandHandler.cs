using AuthModel.Service.Service;
using AuthModule.Contracts.CQRS;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NotificationModule.Contracts.Commands;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace AuthModel.Service.Handler
{
    public class ChangePasswordRequestCommandHandler : IRequestHandler<ChangePasswordRequestCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly OTPService _otpService;

        public ChangePasswordRequestCommandHandler(IUserRepository userRepository, IMediator mediator, OTPService otpService)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _otpService = otpService;
        }

        public async Task<Unit> Handle(ChangePasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFound("User not found");

            var secretKey = user.Key;
            if (string.IsNullOrEmpty(secretKey))
            {
                secretKey = _otpService.GenerateSecretKey();
                user.Key = secretKey;
                await _userRepository.UpdateAsync(user);
            }
            var code = _otpService.GenerateOtp(secretKey);

            var command = new SendChangePasswordMessageCommand(request.Email, code);
            await _mediator.Send(command);

            return Unit.Value;
        }
    }
}
