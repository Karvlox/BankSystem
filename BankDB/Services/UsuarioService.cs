using BankDB.Models;
using BankDB.Repositories;

namespace BankDB.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _usuarioRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _usuarioRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            await _usuarioRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _usuarioRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _usuarioRepository.DeleteUserAsync(id);
        }

        public async Task UpdateUserLicenseAndFaceAsync(int userId, string licensePath, string faceBase64)
        {
            await _usuarioRepository.UpdateLicenseAndFaceAsync(userId, licensePath, faceBase64);
        }

    }
}
