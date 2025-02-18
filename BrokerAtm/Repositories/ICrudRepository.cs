namespace AtmService.Repositories
{
    public interface ICrudRepository<T>
    {
        Task Add(T entity);
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
        Task Delete(Guid id);
    }
}