using BankDB.Models;
using BankDB.Repositories;

namespace BankDB.Services;
public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await _transactionRepository.GetAllTransactionsAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        return await _transactionRepository.GetTransactionByIdAsync(id);
    }

    public async Task<Transaction> AddTransactionAsync(Transaction transaction)
    {
        return await _transactionRepository.AddTransactionAsync(transaction);
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        await _transactionRepository.UpdateTransactionAsync(transaction);
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _transactionRepository.DeleteTransactionAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBySenderIdAsync(int senderId)
    {
        return await _transactionRepository.GetTransactionsBySenderIdAsync(senderId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByReceiverIdAsync(int receiverId)
    {
        return await _transactionRepository.GetTransactionsByReceiverIdAsync(receiverId);
    }

}