using BankApi.Endpoints;
using BankApi.Infrastructure;
using BankApi.Repositories;
using BankApi.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<CreateAccountUseCase>();
builder.Services.AddScoped<GetAllAccountsUseCase>();
builder.Services.AddScoped<DepositUseCase>();
builder.Services.AddScoped<WithdrawUseCase>();

var app = builder.Build();

app.MapAccountEndpoints();

app.Run();