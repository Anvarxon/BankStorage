using BankStorage.Api.DTO;
using BankStorage.Api.Interfaces;
using BankStorage.Api.Services;
using BankStorage.Api.Validators;
using BankStorage.Domain;
using BankStorage.Domain.Models;
using BankStorage.Infrastructure;
using BankStorage.Infrastructure.Context;
using BankStorage.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IRepository<Bank, int>, BankRepository>();
builder.Services.AddScoped<IRepository<Bin_Code, int>, BinCodeRepository>();
builder.Services.AddScoped<IValidator<Bin_Code>, BinCodeValidator>();
builder.Services.AddScoped<IValidator<CardDto>, CardValidator>();
builder.Services.AddTransient<IImageUploadService, ImageUploadService>();

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
