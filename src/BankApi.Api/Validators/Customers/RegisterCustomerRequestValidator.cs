using BankApi.Application.Contracts;
using FluentValidation;

namespace BankApi.Api.Validators.Customers;

public class RegisterCustomerRequestValidator : AbstractValidator<RegisterCustomerRequest>
{
    public RegisterCustomerRequestValidator()
    {
        RuleFor(request => request.FullName)
            .NotEmpty()
            .WithMessage("FullName is required.")
            .MaximumLength(120)
            .WithMessage("FullName must have at most 120 characters.");

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(request => request.Document)
            .NotEmpty()
            .WithMessage("Document is required.")
            .Length(11, 14)
            .WithMessage("Document must have between 11 and 14 characters.");

        RuleFor(request => request.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow.Date))
            .WithMessage("BirthDate cannot be in the future.");

        RuleFor(request => request.Phone)
            .NotEmpty()
            .WithMessage("Phone is required.")
            .MaximumLength(20)
            .WithMessage("Phone must have at most 20 characters.");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must have at least 8 characters.");

        RuleFor(request => request.AccountType)
            .IsInEnum()
            .WithMessage("AccountType is invalid.");
    }
}
