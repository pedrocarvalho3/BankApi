using BankApi.Application.Contracts;

namespace BankApi.Application.UseCases.Interfaces;

public interface ICreateTransferUseCase
{
    Task<CreateTransferResponse> ExecuteAsync(CreateTransferRequest request);
}
