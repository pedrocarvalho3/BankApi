using BankApi.Api.Endpoints;
using BankApi.Application.DependencyInjection;
using BankApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.MapAccountEndpoints();

app.Run();
