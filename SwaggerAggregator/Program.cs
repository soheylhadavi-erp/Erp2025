var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("https://localhost:7220/swagger/v1/swagger.json", "Accounting API");
        options.SwaggerEndpoint("https://localhost:7052/swagger/v1/swagger.json", "General API");

        options.RoutePrefix = "swagger";
        options.DocumentTitle = "API Documentation";
    });
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();