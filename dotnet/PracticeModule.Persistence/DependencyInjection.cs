
using Microsoft.Extensions.DependencyInjection;
using PracticeModule.Contracts.Repositories;
using PracticeModule.Persistence.Repositories;

namespace PracticeModule.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPracticeModulePersistence(this IServiceCollection services)
        {
            services.AddTransient<IPracticeCharacteristicsCommentRepository, PracticeCharacteristicsCommentRepository>();
            services.AddTransient<IPracticeCharacteristicsRepository, PracticeCharacteristicsRepository>();
            services.AddTransient<IPracticeDiaryCommentRepository, PracticeDiaryCommentRepository>();
            services.AddTransient<IPracticeDiaryRepository, PracticeDiaryRepository>();
            services.AddTransient<IGlobalPracticeRepository, GlobalPracticeRepository>();
            services.AddTransient<IPracticeRepository, PracticeRepository>();
        }
    }
}
