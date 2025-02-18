using BankDB.Models;
using Microsoft.EntityFrameworkCore;
using BankDB.Data;

namespace BankDB.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly MiDbContext _context;

    public RoleRepository(MiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
    }

    public async Task AddRoleAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoleAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role != null)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}