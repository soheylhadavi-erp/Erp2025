using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Add YARP reverse proxy and load config from appsettings.json
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Routing
app.UseRouting();

// Map reverse proxy
app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

// Swagger UI only for development
if (app.Environment.IsDevelopment())
{
    // Use Swagger UI for downstream services
    app.UseSwaggerUI(c =>
    {
        // فقط سرویس‌های downstream
        c.SwaggerEndpoint("/swagger/general/v1/swagger.json", "General Service");
        c.SwaggerEndpoint("/swagger/accounting/v1/swagger.json", "Accounting Service");
        c.RoutePrefix = "swagger"; // صفحه Swagger روی /swagger
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
