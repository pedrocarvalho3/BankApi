using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Application.UseCases;

public class RegisterCustomerUseCase : IRegisterCustomerUseCase
{
    private readonly ICustomerAccountUnitOfWork _unitOfWork;

    public RegisterCustomerUseCase(ICustomerAccountUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer> ExecuteAsync(RegisterCustomerRequest request)
    {
        if (await _unitOfWork.Customers.GetByEmailAsync(request.Email) is not null)
            throw new InvalidOperationException("A customer with this email already exists.");

        if (await _unitOfWork.Customers.GetByDocumentAsync(request.Document) is not null)
            throw new InvalidOperationException("A customer with this document already exists.");

        var customer = new Customer(
            fullName: request.FullName,
            email: request.Email,
            document: request.Document,
            birthDate: request.BirthDate,
            phone: request.Phone,
            password: request.Password
        );

        await _unitOfWork.Customers.AddAsync(customer);

        var account = new Account(
            ownerId: customer.Id,
            accountType: request.AccountType
        );
        
        await _unitOfWork.Accounts.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();

        return customer;
    }
}
