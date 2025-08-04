using FluentValidation;
using FluentValidation.AspNetCore;
using ManagementInvoices.Application.Features.Products.Commands.CreateProduct;
using ManagementInvoices.Application.Interfaces;
using ManagementInvoices.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;    
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add EF Core DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly);
});

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

