using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<City> GetAllWithProvince(bool isActive = true)
        {
            return _dbContext.Cities.Include(i => i.Province).Where(w => w.IsActive == isActive).AsQueryable();
        }

        public async Task<bool> ExistWithProvinceCheck(int id)
        {
            var city = await GetNoTracking(id);
            if (city != null)
            {
                var province = await _dbContext.Provinces.FirstOrDefaultAsync(f => f.Id == city.ProvinceId && f.IsActive == true);
                return province != null;
            }
            return false;
        }
    }
}