using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FAQRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}