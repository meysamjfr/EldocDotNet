using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket> GetWithUser(int id);
    }
}
