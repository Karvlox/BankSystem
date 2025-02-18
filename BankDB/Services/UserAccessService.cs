using BankDB.Models;
using BankDB.DTOs;
using BankDB.Repositories;

namespace BankDB.Services;

public class UserAccessService : IUserAccessService
{
    private readonly IUserAccessRepository _userAccessRepository;

    public UserAccessService(IUserAccessRepository userAccessRepository)
    {
        _userAccessRepository = userAccessRepository;
    }

    public async Task<IEnumerable<UserAccess>> GetAllUserAccessAsync()
    {
        return await _userAccessRepository.GetAllUserAccessAsync();
    }

    public async Task<UserAccess?> GetUserAccessByIdAsync(int id)
    {
        return await _userAccessRepository.GetUserAccessByIdAsync(id);
    }

    public async Task AddUserAccessAsync(UserAccess userAccess)
    {
        await _userAccessRepository.AddUserAccessAsync(userAccess);
    }

    public async Task UpdateUserAccessAsync(UserAccess userAccess)
    {
        await _userAccessRepository.UpdateUserAccessAsync(userAccess);
    }

    public async Task DeleteUserAccessAsync(int id)
    {
        await _userAccessRepository.DeleteUserAccessAsync(id);
    }

    public async Task<UserAccess?> GetByNumberAccessAndPasswordAsync(LoginRequestDto loginRequest)
    {
        return await _userAccessRepository.GetByNumberAccessAndPasswordAsync(loginRequest.NumberAccess, loginRequest.Password);
    }

}
