using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}