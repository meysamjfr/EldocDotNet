using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class ExpertRepository : GenericRepository<Expert>, IExpertRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}