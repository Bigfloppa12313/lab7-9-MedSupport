using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Catalog.BLL.Mapping;
using Catalog.BLL.Services.Interfaces;
using Catalog.BLL.Services.Impl;

namespace Catalog.BLL.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogBusinessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}