using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints.Customer;

public static class AuthenticateCustomerEndpoint
{
    public static void MapAuthenticateCustomerEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/customers/authenticate");

        group.MapPost("/", async (AuthenticateCustomerRequest request, IAuthenticateCustomerUseCase useCase) =>
        {
            try
            {
                var token = await useCase.ExecuteAsync(request);
                return Results.Ok(new { token });
            }
            catch (ArgumentException)
            {
                return Results.Unauthorized();
            }
        });
    }
}
