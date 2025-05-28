using DocumentModule.Infrastructure.FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddDocumentModuleInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddDbContext<DocumentModuleDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("HitsInternship")));

            services.AddScoped<FileStorageContext>(provider =>
            {
                FileStorageSettings settings = new FileStorageSettings();
                if (Environment.GetEnvironmentVariable("MINIO_ENDPOINT") == null) configuration.Bind("FileStorageSettings", settings);

                return new FileStorageContext(settings);
            });
        }

        public static void AddDocumentModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<DocumentModuleDbContext>();
            dbContext?.Database.Migrate();
        }
    }
}