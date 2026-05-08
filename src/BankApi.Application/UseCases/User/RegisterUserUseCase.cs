using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.UnitOfWork;

namespace BankApi.Application.UseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserAccountUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserAccountUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> ExecuteAsync(RegisterUserRequest request)
    {
        if (await _unitOfWork.Users.GetByEmailAsync(request.Email) is not null)
            throw new InvalidOperationException("A user with this email already exists.");

        if (await _unitOfWork.Users.GetByDocumentAsync(request.Document) is not null)
            throw new InvalidOperationException("A user with this document already exists.");
        
        var passwordHash = _unitOfWork.PasswordHasher.Hash(request.Password);

        var user = new User(
            fullName: request.FullName,
            email: request.Email,
            document: request.Document,
            birthDate: request.BirthDate,
            phone: request.Phone,
            password: passwordHash
        );

        await _unitOfWork.Users.AddAsync(user);

        var account = new Account(
            ownerId: user.Id,
            accountType: request.AccountType
        );
        
        await _unitOfWork.Accounts.AddAsync(account);

        await _unitOfWork.SaveChangesAsync();

        return user;
    }
}
