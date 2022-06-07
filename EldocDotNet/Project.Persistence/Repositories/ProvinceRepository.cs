using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
    {
        public readonly ApplicationDbContext _dbContext;
        public ProvinceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Province> GetAllWithCity(bool isActive = true)
        {
            return _dbContext.Provinces.Include(i => i.Cities).Where(w => w.IsActive == isActive).AsQueryable();
        }
    }
}