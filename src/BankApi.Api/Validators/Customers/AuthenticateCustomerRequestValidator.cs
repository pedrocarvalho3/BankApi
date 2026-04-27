using BankApi.Application.Contracts;
using FluentValidation;

namespace BankApi.Api.Validators.Customers;

public class AuthenticateCustomerRequestValidator : AbstractValidator<AuthenticateCustomerRequest>
{
    public AuthenticateCustomerRequestValidator()
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
