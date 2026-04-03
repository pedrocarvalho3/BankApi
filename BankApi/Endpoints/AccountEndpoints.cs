using BankApi.Contracts;
using BankApi.UseCases;

namespace BankApi.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/", (GetAllAccountsUseCase useCase) =>
        {
            var accounts = useCase.Execute();
            return Results.Ok(accounts);
        });

        group.MapPost("/", (CreateAccountRequest request, CreateAccountUseCase useCase) =>
        {
            var account = useCase.Execute(request);
            return Results.Created($"/accounts/{account.Id}", account);
        });

        group.MapPost("/deposit", (CreateInternalTransactionRequest request, DepositUseCase useCase) =>
        {
            var account = useCase.Execute(request);

            return Results.Ok(account);
        });

        group.MapPost("/withdraw", (CreateInternalTransactionRequest request, WithdrawUseCase useCase) =>
        {
            var account = useCase.Execute(request);
            return Results.Ok(account);
        });
    }
}