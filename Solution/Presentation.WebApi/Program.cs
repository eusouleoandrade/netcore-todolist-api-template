
using Core.Application.Ioc;
using Infra.Persistence.Ioc;
using Presentation.WebApi.Extensions;

// Configure services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

// Configure app
var app = builder.Build();

app.UseSwaggerExtension();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Configure services provider
IServiceScope scope = app.Services.CreateScope();

scope.ServiceProvider.ConfigureDataBaseBootstrap();