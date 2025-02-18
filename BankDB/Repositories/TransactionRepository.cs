using BankDB.Models;
using BankDB.Data;
using Microsoft.EntityFrameworkCore;

namespace BankDB.Repositories;
public class TransactionRepository : ITransactionRepository
{
    private readonly MiDbContext _context;

    public TransactionRepository(MiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Include(t => t.TypeTransaction)
            .ToListAsync();
    }

    public async Task<Transaction> GetTransactionByIdAsync(int id)
    {
        return await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Include(t => t.TypeTransaction)
            .FirstOrDefaultAsync(t => t.TransactionId == id);
    }

    public async Task<Transaction> AddTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        _context.Entry(transaction).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsBySenderIdAsync(int senderId)
    {
        return await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Include(t => t.TypeTransaction)
            .Where(t => t.SenderId == senderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByReceiverIdAsync(int receiverId)
    {
        return await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Include(t => t.TypeTransaction)
            .Where(t => t.RecivedId == receiverId)
            .ToListAsync();
    }
}