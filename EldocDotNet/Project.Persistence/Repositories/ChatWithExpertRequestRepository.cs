using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class ChatWithExpertRequestRepository : GenericRepository<ChatWithExpertRequest>, IChatWithExpertRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatWithExpertRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}