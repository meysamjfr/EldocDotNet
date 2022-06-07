namespace Project.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task Recover(int id);
        Task<bool> Exist(int id);
        System.Linq.IQueryable<T> GetAllQueryable();
        Task<T> GetNoTracking(int id);
    }
}
