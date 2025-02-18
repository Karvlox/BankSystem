using BankDB.Models;
using Microsoft.EntityFrameworkCore;
using BankDB.Data;

namespace BankDB.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly MiDbContext _context;

    public UsuarioRepository(MiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Accounts)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddUserAsync(User user)
    {
        user.Accounts = null;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateLicenseAndFaceAsync(int userId, string licensePath, string faceBase64)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.LicensePath = licensePath;
            user.FaceBase64 = faceBase64;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

}
