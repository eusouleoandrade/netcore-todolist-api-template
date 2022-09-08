using Microsoft.OpenApi.Models;

namespace Presentation.WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo List v1",
                    Description = "Your todo list v1."
                });
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo List - V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}