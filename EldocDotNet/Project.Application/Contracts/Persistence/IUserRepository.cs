using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByPhone(string phone);
        Task<User> GetByToken(string token);
        Task<User> GetWithRelations(int id);
    }
}
