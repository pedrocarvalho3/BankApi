using BankApi.Interfaces;
using BankApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IWelcomeService, WelcomeService>();

var app = builder.Build();

app.MapGet("/", (IWelcomeService welcomeService1, IWelcomeService welcomeService2) =>
{
    string message1 = $"Message1: {welcomeService1.GetWelcomeMessage()}";
    string message2 = $"Message2: {welcomeService2.GetWelcomeMessage()}";
    
    return $"{message1}\n{message2}";
});

app.Run();