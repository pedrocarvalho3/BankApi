using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints.Customer;

public static class CreateCustomerEndpoint
{
    public static void MapCreateCustomerEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/customers");

        group.MapPost("/", async (RegisterCustomerRequest request, IRegisterCustomerUseCase useCase) =>
        {
            try
            {
                var customer = await useCase.ExecuteAsync(request);
                return Results.Created($"/customers/{customer.Id}", customer);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { message = ex.Message });
            }
        });
    }
}
