using BankApi.Application.Contracts;
using BankApi.Application.UseCases.Interfaces;

namespace BankApi.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/users")
            .WithTags("Users");

        group.MapPost("/", async (RegisterUserRequest request, IRegisterUserUseCase useCase) =>
            {
                try
                {
                    var user = await useCase.ExecuteAsync(request);
                    return Results.Created($"/api/v1/users/{user.Id}", user);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { message = ex.Message });
                }
            })
            .WithName("CreateUser")
            .WithSummary("Create user")
            .WithDescription("Registers a new user and creates the initial account.")
            .Validate<RegisterUserRequest>()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict);
        

        group.MapPost("/auth/login", async (AuthenticateUserRequest request, IAuthenticateUserUseCase useCase) =>
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
            .WithName("AuthenticateUser")
            .WithSummary("Authenticate user")
            .WithDescription("Authenticates user credentials and returns a JWT token.")
            .Validate<AuthenticateUserRequest>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
