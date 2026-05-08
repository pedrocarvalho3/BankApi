using BankApi.Application.Contracts;
using BankApi.Core.Entities;

namespace BankApi.Application.UseCases.Interfaces;

public interface IRegisterUserUseCase
{
    Task<User> ExecuteAsync(RegisterUserRequest request);
}
