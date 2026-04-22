namespace BankApi.Api.Endpoints;

public static class ApiEndpoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapAccountEndpoints();
        app.MapCustomerEndpoints();
    }
}
