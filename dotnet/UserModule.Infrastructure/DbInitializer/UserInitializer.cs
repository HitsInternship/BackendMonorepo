using System.Security.Cryptography;
using AuthModel.Service.Interface;
using AuthModule.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using UserInfrastructure;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Infrastructure.DbInitializer;

public class UserInitializer : IUserInitializer
{
    private readonly UserModuleDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly AuthDbContext _authDbContext;
    private readonly IHashService _hashService;

    public UserInitializer(UserModuleDbContext context, IUserRepository userRepository, IRoleRepository roleRepository, AuthDbContext authDbContext, IHashService hashService)
    {
        _context = context;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _authDbContext = authDbContext;
        _hashService = hashService;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during migration: {ex.Message}");
        }

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var existingUser = await _userRepository.GetByEmailAsync("admin@admin.com");

            if (existingUser == null)
            {
                var user = new User()
                {
                    Email = "admin@admin.com",
                    Name = "Admin",
                    Surname = "Admin",
                };

                var roles = await _roleRepository.GetRolesAsync(new List<RoleName>
                {
                    RoleName.Candidate, RoleName.Student, RoleName.Curator, RoleName.Intern,
                    RoleName.CompanyRepresenter, RoleName.DeanMember
                });

                foreach (var role in roles.Where(role => !user.Roles.Contains(role)))
                {
                    user.Roles.Add(role);
                }

                await _userRepository.AddAsync(user);
                
                using SHA256 sha256Hash = SHA256.Create();
                _authDbContext.AspNetUsers.Add(new AspNetUser()
                {
                    Id = Guid.NewGuid(),
                    Login = user.Email,
                    Password = _hashService.GetHash(sha256Hash, "Admin"),
                    UserId = user.Id,
                });
                await _authDbContext.SaveChangesAsync();
            }
        }
    }
}