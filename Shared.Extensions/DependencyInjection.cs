using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Configs;
using Shared.Contracts.Dtos;
using Shared.Persistence;

namespace Shared.Extensions;

public static class DependencyInjection
{
    public static void AddSharedModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGenericRepositories();

        services.Configure<PaginationConfig>(configuration.GetSection("Pagination"));
        
        services.AddSwaggerGen(options =>
        {
            var sharedDtos = $"{typeof(CommentDto).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, sharedDtos));
        });
    }
}