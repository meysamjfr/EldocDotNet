using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class FinancialContractRepository : GenericRepository<FinancialContract>, IFinancialContractRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FinancialContractRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}