using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentModule.Application
{
    public static class DependencyInjection
    {
        public static void AddStudentModuleApplication(this IServiceCollection services)
        {
            services.AddMediatR(config
                => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}