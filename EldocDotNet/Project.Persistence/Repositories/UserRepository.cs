using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByPhone(string phone)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Phone == phone);
        }

        public async Task<User> GetByToken(string token)
        {
            if (token == "UNKOWN") return null;
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Token == token);
        }

        public async Task<User> GetWithRelations(int id)
        {
            var user = await GetNoTracking(id);

            return user;
        }

    }
}