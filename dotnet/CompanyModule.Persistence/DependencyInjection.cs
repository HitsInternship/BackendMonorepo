using CompanyModule.Contracts.Repositories;
using CompanyModule.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyModule.Persistence
{
    public static class DependencyInjection
    {
        public static void AddCompanyModulePersistence(this IServiceCollection services)
        {
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IPartnershipAgreementRepository, PartnershipAgreementRepository>();
            services.AddTransient<ICuratorRepository, CuratorRepository>();
            services.AddTransient<ITimeslotRepository, TimeslotRepository>();
        }
    }
}
