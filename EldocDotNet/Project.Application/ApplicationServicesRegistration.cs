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
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
