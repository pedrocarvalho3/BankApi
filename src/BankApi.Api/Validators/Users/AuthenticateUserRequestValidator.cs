using BankApi.Application.Contracts;
using FluentValidation;

namespace BankApi.Api.Validators.Users;

public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
