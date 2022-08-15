using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contracts.Persistence;
using Project.Persistence.Repositories;

namespace Project.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseLazyLoadingProxies();
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.UseNetTopologySuite());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<IUnilateralContractRepository, UnilateralContractRepository>();
            services.AddScoped<IBilateralContractRepository, BilateralContractRepository>();
            services.AddScoped<IFinancialContractRepository, FinancialContractRepository>();
            services.AddScoped<IUnilateralContractTemplateRepository, UnilateralContractTemplateRepository>();
            services.AddScoped<IBilateralContractTemplateRepository, BilateralContractTemplateRepository>();
            services.AddScoped<IFinancialContractTemplateRepository, FinancialContractTemplateRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();

            return services;
        }
    }
}