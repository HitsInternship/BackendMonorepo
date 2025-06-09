using AuthModel.Service.Interface;
using AuthModel.Service.Service;
using AuthModule.Contracts.CQRS;
using AuthModule.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserInfrastructure;
using UserModule.Contracts.Repositories;

namespace AuthModel.Service.Handler
{
    public class ChangePasswordConfirmCommandHandler : IRequestHandler<ChangePasswordConfirmCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly OTPService _otpService;
        private readonly AuthDbContext _context;
        private readonly IHashService _hashService;

        public ChangePasswordConfirmCommandHandler(IUserRepository userRepository, IMediator mediator, OTPService otpService, AuthDbContext context, IHashService hashService)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _otpService = otpService;
            _context = context;
            _hashService = hashService;
        }

        public async Task<Unit> Handle(ChangePasswordConfirmCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFound("User not found");

            var secretKey = user.Key;
            if (string.IsNullOrEmpty(secretKey)) throw new BadRequest("Неверный запрос");

            if (!_otpService.VerifyOtp(secretKey, request.Code))
            {
                throw new BadRequest("Неверный код подтверждения");
            }

            var aspUser = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Login == request.Email);

            using SHA256 sha256Hash = SHA256.Create();
            var hash = _hashService.GetHash(sha256Hash, request.Password);
            aspUser.Password = hash;
            await _context.SaveChangesAsync(cancellationToken);

            user.Key = null;
            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
