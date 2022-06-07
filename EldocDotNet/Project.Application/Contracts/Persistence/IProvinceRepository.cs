using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface IProvinceRepository : IGenericRepository<Province>
    {
        IQueryable<Province> GetAllWithCity(bool isActive = true);
    }
}
