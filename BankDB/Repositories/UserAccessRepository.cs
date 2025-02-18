using BankDB.Models;
using Microsoft.EntityFrameworkCore;
using BankDB.Data;

namespace BankDB.Repositories;

public class UserAccessRepository : IUserAccessRepository
{
    private readonly MiDbContext _context;

    public UserAccessRepository(MiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserAccess>> GetAllUserAccessAsync()
    {
        return await _context.UserAccesses.Include(ua => ua.User).Include(ua => ua.Role).ToListAsync();
    }

    public async Task<UserAccess?> GetUserAccessByIdAsync(int id)
    {
        return await _context.UserAccesses
            .Include(ua => ua.User)
            .Include(ua => ua.Role)
            .FirstOrDefaultAsync(ua => ua.UserAccessId == id);
    }

    public async Task AddUserAccessAsync(UserAccess userAccess)
    {
        await _context.UserAccesses.AddAsync(userAccess);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAccessAsync(UserAccess userAccess)
    {
        _context.UserAccesses.Update(userAccess);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAccessAsync(int id)
    {
        var userAccess = await _context.UserAccesses.FindAsync(id);
        if (userAccess != null)
        {
            _context.UserAccesses.Remove(userAccess);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UserAccess?> GetByNumberAccessAsync(int numberAccess)
    {
        return await _context.UserAccesses.FirstOrDefaultAsync(ua => ua.NumberAccess == numberAccess);
    }

    public async Task<UserAccess?> GetByNumberAccessAndPasswordAsync(int numberAccess, string password)
    {
        return await _context.UserAccesses
            .Include(ua => ua.User)
            .Include(ua => ua.Role)
            .FirstOrDefaultAsync(ua => ua.NumberAccess == numberAccess && ua.Password == password);
    }

}