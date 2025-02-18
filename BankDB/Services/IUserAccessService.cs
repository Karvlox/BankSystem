using BankDB.Models;
using BankDB.DTOs;

namespace BankDB.Services;

public interface IUserAccessService
{
    Task<IEnumerable<UserAccess>> GetAllUserAccessAsync();
    Task<UserAccess?> GetUserAccessByIdAsync(int id);
    Task AddUserAccessAsync(UserAccess userAccess);
    Task UpdateUserAccessAsync(UserAccess userAccess);
    Task DeleteUserAccessAsync(int id);
    Task<UserAccess?> GetByNumberAccessAndPasswordAsync(LoginRequestDto loginRequest);
}