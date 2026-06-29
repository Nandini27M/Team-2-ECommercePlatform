using ECommerce.ShippingService.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShippingService.Consumers;
using ShippingService.Middleware;
using ShippingService.Services;
using ShippingService.Validators;
using SmartBank.ShippingService.Repositories;
using SShippingService.Data;

var builder = WebApplication.CreateBuilder(args);

// =========================
// Serilog Configuration
// =========================
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/shipping-log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// =========================
// Database
// =========================
builder.Services.AddDbContext<ShippingDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// =========================
// Controllers
// =========================
builder.Services.AddControllers();

// =========================
// Swagger
// =========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =========================
// Fluent Validation
// =========================
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateShippingRequestValidator>();

// =========================
// Dependency Injection
// =========================
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();

builder.Services.AddScoped<IShippingService, ShippingService.Services.ShippingService>();

// =========================
// RabbitMQ Consumer
// =========================
builder.Services.AddHostedService<PaymentCompletedConsumer>();

var app = builder.Build();

// =========================
// Middleware
// =========================
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();