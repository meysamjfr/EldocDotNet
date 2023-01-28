using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface IContractRepository : IGenericRepository<Contract>
    {
        Task<Contract> GetContract(int bargainCode);
        Task<Contract> GetContract(int userId, int bargainCode);
    }
}
