using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;
using BankApi.Core;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class AuthenticateUserUseCase : IAuthenticateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthenticateUserUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenGenerator tokenGenerator)
    {
        _userRepository =  userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<string> ExecuteAsync(AuthenticateUserRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user is null)
            throw new ArgumentException("A user with this email doesn't exist.");
        
        if (_passwordHasher.Verify(request.Password, user.Password) == false)
            throw new ArgumentException("Invalid Credentials.");

        return _tokenGenerator.Generate(user.Id, user.Email);
    }
}
