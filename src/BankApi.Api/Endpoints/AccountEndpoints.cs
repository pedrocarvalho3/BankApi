using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/", (IGetAllAccountsUseCase useCase) =>
        {
            var accounts = useCase.Execute();
            return Results.Ok(accounts);
        });

        group.MapPost("/", (CreateAccountRequest request, ICreateAccountUseCase useCase) =>
        {
            var account = useCase.Execute(request);
            return Results.Created($"/accounts/{account.Id}", account);
        });

        group.MapPost("/deposit", (CreateInternalTransactionRequest request, IDepositUseCase useCase) =>
        {
            var account = useCase.Execute(request);

            return Results.Ok(account);
        });

        group.MapPost("/withdraw", (CreateInternalTransactionRequest request, IWithdrawUseCase useCase) =>
        {
            var account = useCase.Execute(request);
            return Results.Ok(account);
        });
    }
}
