using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class BilateralContractTemplateRepository : GenericRepository<BilateralContractTemplate>, IBilateralContractTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BilateralContractTemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}