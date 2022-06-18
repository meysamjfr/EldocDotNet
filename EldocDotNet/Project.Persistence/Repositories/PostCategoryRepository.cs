using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class PostCategoryRepository : GenericRepository<PostCategory>, IPostCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}