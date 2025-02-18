using BankDB.Models;
using BankDB.Repositories;

namespace BankDB.Services
{
    public class TypeTransactionService : ITypeTransactionService
    {
        private readonly ITypeTransactionRepository _typeTransactionRepository;

        public TypeTransactionService(ITypeTransactionRepository typeTransactionRepository)
        {
            _typeTransactionRepository = typeTransactionRepository;
        }

        public async Task<IEnumerable<TypeTransaction>> GetAllTypeTransactionsAsync()
        {
            return await _typeTransactionRepository.GetAllTypeTransactionsAsync();
        }

        public async Task<TypeTransaction?> GetTypeTransactionByIdAsync(int id)
        {
            return await _typeTransactionRepository.GetTypeTransactionByIdAsync(id);
        }

        public async Task<TypeTransaction> AddTypeTransactionAsync(TypeTransaction typeTransaction)
        {
            return await _typeTransactionRepository.AddTypeTransactionAsync(typeTransaction);
        }

        public async Task UpdateTypeTransactionAsync(TypeTransaction typeTransaction)
        {
            await _typeTransactionRepository.UpdateTypeTransactionAsync(typeTransaction);
        }

        public async Task DeleteTypeTransactionAsync(int id)
        {
            await _typeTransactionRepository.DeleteTypeTransactionAsync(id);
        }
    }
}
