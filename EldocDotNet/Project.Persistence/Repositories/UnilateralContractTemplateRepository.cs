using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class UnilateralContractTemplateRepository : GenericRepository<UnilateralContractTemplate>, IUnilateralContractTemplateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UnilateralContractTemplateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}