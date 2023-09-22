
using Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Service.Handlers;
using Service.Queries;
using Service.Responses;

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
