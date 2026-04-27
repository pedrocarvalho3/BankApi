using BankApi.Application.Contracts;
using FluentValidation;

namespace BankApi.Api.Validators.Accounts;

public class CreateTransferRequestValidator : AbstractValidator<CreateTransferRequest>
{
    public CreateTransferRequestValidator()
    {
        RuleFor(request => request.SourceAccountId)
            .NotEmpty()
            .WithMessage("SourceAccountId is required.");

        RuleFor(request => request.DestinationAccountId)
            .NotEmpty()
            .WithMessage("DestinationAccountId is required.");

        RuleFor(request => request.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");

        RuleFor(request => request)
            .Must(request => request.SourceAccountId != request.DestinationAccountId)
            .WithMessage("Source and destination accounts must be different.");
    }
}
