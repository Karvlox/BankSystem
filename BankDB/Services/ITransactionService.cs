using BankDB.Models;

namespace BankDB.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<Transaction> GetTransactionByIdAsync(int id);
    Task<Transaction> AddTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(int id);
    Task<IEnumerable<Transaction>> GetTransactionsBySenderIdAsync(int senderId);
    Task<IEnumerable<Transaction>> GetTransactionsByReceiverIdAsync(int receiverId);
}