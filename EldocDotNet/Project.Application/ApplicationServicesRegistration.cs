using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
using Project.Application.Profiles;
using System.Reflection;

namespace Project.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new MappingProfile(geometryFactory));
            }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices
                .Instance.CreateGeometryFactory(srid: 4326));

            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<IUnilateralContractTemplateService, UnilateralContractTemplateService>();
            services.AddScoped<IBilateralContractTemplateService, BilateralContractTemplateService>();
            services.AddScoped<IFinancialContractTemplateService, FinancialContractTemplateService>();
            services.AddScoped<IExpertService, ExpertService>();
            services.AddScoped<IChatWithExpertRequestService, ChatWithExpertRequestService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IChatWithExpertService, ChatWithExpertService>();
            services.AddScoped<IChatWithExpertMessageService, ChatWithExpertMessageService>();

            return services;
        }
    }
}
