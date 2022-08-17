using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class ChatWithExpertMessageRepository : GenericRepository<ChatWithExpertMessage>, IChatWithExpertMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatWithExpertMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}