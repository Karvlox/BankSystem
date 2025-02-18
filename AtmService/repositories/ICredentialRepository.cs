namespace AtmService.Repositories
{
    public interface ICredentialRepository
    {
        Task<bool> WriteCredentials(Guid guid);
        Task<Guid> ReadCredentials();
    }
}