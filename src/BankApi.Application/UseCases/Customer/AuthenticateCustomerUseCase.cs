using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class AuthenticateCustomerUseCase : IAuthenticateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticateCustomerUseCase(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
    {
        _customerRepository =  customerRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<string> ExecuteAsync(AuthenticateCustomerRequest request)
    {
        var customer = await _customerRepository.GetByEmailAsync(request.Email);
        
        if (customer is null)
            throw new ArgumentException("A customer with this email doesn't exist.");
        
        if (_passwordHasher.Verify(request.Password, customer.Password) == false)
            throw new ArgumentException("Invalid Credentials.");

        return customer.Email;
    }
}