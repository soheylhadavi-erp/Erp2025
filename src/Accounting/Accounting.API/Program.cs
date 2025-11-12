var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// اضافه کردن CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerAggregator", policy =>
    {
        policy.WithOrigins("https://localhost:7220") // آدرس SwaggerAggregator و سایر API ها
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// استفاده از CORS 
app.UseCors("AllowSwaggerAggregator");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
