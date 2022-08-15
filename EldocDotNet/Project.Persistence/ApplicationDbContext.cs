using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Entities.Base;

namespace Project.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.Now;
                entry.Entity.UpdatedBy = "SYSTEM";

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = "SYSTEM";
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BilateralContract> BilateralContracts { get; set; }
        public DbSet<UnilateralContract> UnilateralContracts { get; set; }
        public DbSet<FinancialContract> FinancialContracts { get; set; }
        public DbSet<BilateralContractTemplate> BilateralContractTemplates { get; set; }
        public DbSet<UnilateralContractTemplate> UnilateralContractTemplates { get; set; }
        public DbSet<FinancialContractTemplate> FinancialContractTemplates { get; set; }
        public DbSet<Expert> Experts { get; set; }
    }
}
