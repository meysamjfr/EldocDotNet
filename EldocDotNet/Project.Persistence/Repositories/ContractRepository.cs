using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.Exceptions;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContractRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contract> GetContract(int bargainCode)
        {
            var find = await GetAllQueryable()
                   .AsNoTracking()
                   .Include(i => i.User)
                   .FirstOrDefaultAsync(f => f.IsActive == true && f.BargainCode == bargainCode);

            if (find == null)
            {
                throw new NotFoundException("قرارداد مورد نظر پیدا نشد");
            }

            return find;
        }

        public async Task<Contract> GetContract(int userId, int bargainCode)
        {
            var find = await GetAllQueryable()
                   .AsNoTracking()
                   .Include(i => i.User)
                   .FirstOrDefaultAsync(f => f.IsActive == true && f.BargainCode == bargainCode && f.UserId == userId);

            if (find == null)
            {
                throw new NotFoundException("قرارداد مورد نظر پیدا نشد");
            }

            return find;
        }
    }
}