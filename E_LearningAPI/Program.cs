using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using E_Learning_Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//-------------------------------------------------------------------------------------------------

// Here I add Cors Policy
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("OpenCorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

// Here I add reference to the Entity Framework
builder.Services.AddDbContext<ELearningDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

    // When we are still in development, we can log some additional info that can help us
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors(); // To get field-level error details
        options.EnableSensitiveDataLogging();
        options.ConfigureWarnings(warningsAction =>
        {
            warningsAction.Log(new EventId[]
            {
            CoreEventId.FirstWithoutOrderByAndFilterWarning,
            CoreEventId.RowLimitingOperationWithoutOrderByWarning
            });
        });
    }
});
//-------------------------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
