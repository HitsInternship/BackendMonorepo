using Microsoft.Extensions.DependencyInjection;
using StudentModule.Contracts.Repositories;
using StudentModule.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Persistence
{
    public static class DependencyInjection
    {
        public static void AddStudentModulePersistence(this IServiceCollection services)
        {
            services.AddTransient<IStreamRepository, StreamRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
        }
    }
}
