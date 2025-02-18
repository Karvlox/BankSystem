using BankDB.Models;

namespace BankDB.Repositories;

public interface IUserAccessRepository
{
    Task<IEnumerable<UserAccess>> GetAllUserAccessAsync();
    Task<UserAccess?> GetUserAccessByIdAsync(int id);
    Task AddUserAccessAsync(UserAccess userAccess);
    Task UpdateUserAccessAsync(UserAccess userAccess);
    Task DeleteUserAccessAsync(int id);
    Task<UserAccess?> GetByNumberAccessAsync(int numberAccess);
    Task<UserAccess?> GetByNumberAccessAndPasswordAsync(int numberAccess, string password);
}