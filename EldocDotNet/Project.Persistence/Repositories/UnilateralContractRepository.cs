using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class UnilateralContractRepository : GenericRepository<UnilateralContract>, IUnilateralContractRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UnilateralContractRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}