using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<bool> ExistWithProvinceCheck(int id);
        IQueryable<City> GetAllWithProvince(bool isActive = true);
    }
}
