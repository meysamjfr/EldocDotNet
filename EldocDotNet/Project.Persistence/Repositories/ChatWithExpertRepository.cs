using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class ChatWithExpertRepository : GenericRepository<ChatWithExpert>, IChatWithExpertRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatWithExpertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}