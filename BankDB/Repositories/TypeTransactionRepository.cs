using BankDB.Models;
using BankDB.Data;
using Microsoft.EntityFrameworkCore;

namespace BankDB.Repositories
{
    public class TypeTransactionRepository : ITypeTransactionRepository
    {
        private readonly MiDbContext _context;

        public TypeTransactionRepository(MiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeTransaction>> GetAllTypeTransactionsAsync()
        {
            return await _context.TypeTransactions.ToListAsync();
        }

        public async Task<TypeTransaction?> GetTypeTransactionByIdAsync(int id)
        {
            return await _context.TypeTransactions.FindAsync(id);
        }

        public async Task<TypeTransaction> AddTypeTransactionAsync(TypeTransaction typeTransaction)
        {
            _context.TypeTransactions.Add(typeTransaction);
            await _context.SaveChangesAsync();
            return typeTransaction;
        }

        public async Task UpdateTypeTransactionAsync(TypeTransaction typeTransaction)
        {
            _context.Entry(typeTransaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeTransactionAsync(int id)
        {
            var typeTransaction = await _context.TypeTransactions.FindAsync(id);
            if (typeTransaction != null)
            {
                _context.TypeTransactions.Remove(typeTransaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
