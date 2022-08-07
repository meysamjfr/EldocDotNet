using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class FinancialContractTemplateRepository : GenericRepository<FinancialContractTemplate>, IFinancialContractTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FinancialContractTemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}