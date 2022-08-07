using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class BilateralContractRepository : GenericRepository<BilateralContract>, IBilateralContractRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BilateralContractRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}