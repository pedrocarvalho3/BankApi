namespace BankApi.Core;

public interface ITokenGenerator
{
    string Generate(Guid customerId, string email);
}
