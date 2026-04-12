using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IRegisterCustomerUseCase
{
    Task<Customer> ExecuteAsync(RegisterCustomerRequest request);
}
