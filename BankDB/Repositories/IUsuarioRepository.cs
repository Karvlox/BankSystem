using BankDB.Models;

namespace BankDB.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateLicenseAndFaceAsync(int userId, string licensePath, string faceBase64);
    }
}
