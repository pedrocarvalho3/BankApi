using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/customers")
            .WithTags("Customers");

        group.MapPost("/", async (RegisterCustomerRequest request, IRegisterCustomerUseCase useCase) =>
            {
                try
                {
                    var customer = await useCase.ExecuteAsync(request);
                    return Results.Created($"/api/v1/customers/{customer.Id}", customer);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { message = ex.Message });
                }
            })
            .WithName("CreateCustomer")
            .WithSummary("Create customer")
            .WithDescription("Registers a new customer and creates the initial account.")
            .Validate<RegisterCustomerRequest>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict);

        group.MapPost("/authenticate", async (AuthenticateCustomerRequest request, IAuthenticateCustomerUseCase useCase) =>
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
            })
            .WithName("AuthenticateCustomer")
            .WithSummary("Authenticate customer")
            .WithDescription("Authenticates customer credentials and returns a JWT token.")
            .Validate<AuthenticateCustomerRequest>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
