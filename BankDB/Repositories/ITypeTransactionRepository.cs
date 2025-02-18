using BankDB.Models;

namespace BankDB.Repositories;

public interface ITypeTransactionRepository
{
    Task<IEnumerable<TypeTransaction>> GetAllTypeTransactionsAsync();
    Task<TypeTransaction?> GetTypeTransactionByIdAsync(int id);
    Task<TypeTransaction> AddTypeTransactionAsync(TypeTransaction typeTransaction);
    Task UpdateTypeTransactionAsync(TypeTransaction typeTransaction);
    Task DeleteTypeTransactionAsync(int id);
}