using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/", async (IGetAllAccountsUseCase useCase) =>
        {
            var accounts = await useCase.ExecuteAsync();
            return Results.Ok(accounts);
        });

        group.MapPost("/", async (CreateAccountRequest request, ICreateAccountUseCase useCase) =>
        {
            var account = await useCase.ExecuteAsync(request);
            return Results.Created($"/accounts/{account.Id}", account);
        });

        group.MapPost("/deposit", async (CreateInternalTransactionRequest request, IDepositUseCase useCase) =>
        {
            var account = await useCase.ExecuteAsync(request);

            return Results.Ok(account);
        });

        group.MapPost("/withdraw", async (CreateInternalTransactionRequest request, IWithdrawUseCase useCase) =>
        {
            var account = await useCase.ExecuteAsync(request);
            return Results.Ok(account);
        });
    }
}
