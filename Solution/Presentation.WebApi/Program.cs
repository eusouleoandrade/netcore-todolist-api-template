
// Configure services
using Presentation.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();

// Configure app
var app = builder.Build();

app.UseSwaggerExtension();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();