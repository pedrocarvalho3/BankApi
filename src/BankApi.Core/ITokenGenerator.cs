namespace BankApi.Core;

public interface ITokenGenerator
{
    string Generate(Guid userId, string email);
}
