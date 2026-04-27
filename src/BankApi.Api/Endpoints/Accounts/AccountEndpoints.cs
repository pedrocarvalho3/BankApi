using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/accounts")
            .WithTags("Accounts")
            .RequireAuthorization();

        group.MapGet("/", async (IGetAllAccountsUseCase useCase) =>
            {
                var accounts = await useCase.ExecuteAsync();
                return Results.Ok(accounts);
            })
            .WithName("GetAccounts")
            .WithSummary("List accounts")
            .WithDescription("Returns all accounts for authorized users.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/deposit", async (CreateInternalTransactionRequest request, ICreateDepositUseCase useCase) =>
            {
                var account = await useCase.ExecuteAsync(request);
                return Results.Ok(account);
            })
            .WithName("Deposit")
            .WithSummary("Deposit amount")
            .WithDescription("Deposits funds into an account.")
            .Validate<CreateInternalTransactionRequest>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/withdraw", async (CreateInternalTransactionRequest request, ICreateWithdrawUseCase useCase) =>
            {
                var account = await useCase.ExecuteAsync(request);
                return Results.Ok(account);
            })
            .WithName("Withdraw")
            .WithSummary("Withdraw amount")
            .WithDescription("Withdraws funds from an account.")
            .Validate<CreateInternalTransactionRequest>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/transfer", async (CreateTransferRequest request, ICreateTransferUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(request);
                    return Results.Ok(result);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            })
            .WithName("Transfer")
            .WithSummary("Transfer amount")
            .WithDescription("Transfers funds between two accounts and records debit/credit transactions.")
            .Validate<CreateTransferRequest>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
