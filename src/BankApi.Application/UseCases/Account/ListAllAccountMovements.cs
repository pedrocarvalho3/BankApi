using BankApi.Application.UseCases.Interfaces;
using BankApi.Core.Entities;
using BankApi.Core.Interfaces.Repositories;

namespace BankApi.Application.UseCases;

public class ListAllAccountMovements : IListAllAccountMovements
{
    private readonly ITransactionRepository _transactionRepository;

    public ListAllAccountMovements(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    public async Task<List<Transaction>> ExecuteAsync(Guid accountId)
    {
        return await _transactionRepository.GetAllByAccountIdAsync(accountId);
    }
}