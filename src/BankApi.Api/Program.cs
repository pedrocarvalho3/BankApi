using BankApi.Api.Endpoints;
using BankApi.Api.Endpoints.Customer;
using BankApi.Application.DependencyInjection;
using BankApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Bank Api - V1";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank Api - V1");
    });
}

app.MapAccountEndpoints();
app.MapCreateUserEndpoint();

app.Run();
