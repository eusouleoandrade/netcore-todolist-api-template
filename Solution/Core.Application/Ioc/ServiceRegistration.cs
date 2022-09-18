using Core.Application.Interfaces.UseCases;
using Core.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application.Ioc
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IGetAllTodoUseCase, GetAllTodoUseCase>();
            services.AddScoped<ICreateTodoUseCase, CreateTodoUseCase>();
        }
    }
}