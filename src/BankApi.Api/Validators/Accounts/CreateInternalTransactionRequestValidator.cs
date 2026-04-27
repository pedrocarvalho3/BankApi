using BankApi.Application.Contracts;
using FluentValidation;

namespace BankApi.Api.Validators.Accounts;

public class CreateInternalTransactionRequestValidator : AbstractValidator<CreateInternalTransactionRequest>
{
    public CreateInternalTransactionRequestValidator()
    {
        RuleFor(request => request.accountId)
            .NotEmpty()
            .WithMessage("AccountId is required.");

        RuleFor(request => request.amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");
    }
}
