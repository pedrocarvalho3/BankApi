using BankApi.Interfaces;

namespace BankApi.Services;

public class WelcomeService : IWelcomeService
{
    DateTime _serviceCreated;
    Guid _serviceId;
    
    public WelcomeService()
    {
        _serviceCreated = DateTime.UtcNow;
        _serviceId = Guid.NewGuid();
    }

    public string GetWelcomeMessage()
    {
        return $"Welcome to my bank! The current time is {_serviceCreated}. This service instance has an ID of {_serviceId}";
    }
}