using Core.Application.Interfaces.Repositories;
using Infra.Persistence.Interfaces;
using Infra.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.Ioc
{
    public static class ServiceRegistration
    {
        public static void ConfigureDataBaseBootstrap(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IDatabaseBootstrap>()?.Setup();
        }

        public static void AddPersistenceLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));
            services.AddScoped<ITodoRepositoryAsync, TodoRepositoryAsync>();
        }
    }
}