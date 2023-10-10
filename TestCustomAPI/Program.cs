
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Service.Command;
using Service.Handlers;
using Service.Queries;
using Service.Responses;
using Swashbuckle.AspNetCore.Filters;
using TestCustomAPI.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Regis DB
builder.Services.RegisterContextDb(builder.Configuration);

// Regis Dependency Injection
builder.Services.RegisterDI();

// Add MediatR
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOQueryHandler).Assembly));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOQuery).Assembly));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(GetAllMachineDTOResponse).Assembly));
//builder.Services.AddMediatR(typeof(LoginUserCommandHandler));


// Regis Token
builder.Services.RegisTokenBearer(builder.Configuration);

//Fluent Validator
//builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginUserCommand>());

// Gen Authorization Swagger Icon
builder.Services.RegisSwaggerGen();


// Add Authorization Role
builder.Services.RegisRole();


builder.Services.RegisterFluentValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
