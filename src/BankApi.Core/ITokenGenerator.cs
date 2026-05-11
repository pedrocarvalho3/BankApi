namespace BankApi.Core;

public interface ITokenGenerator
{
    string Generate(Guid userId, Guid accountId, string email);
}
