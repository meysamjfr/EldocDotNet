using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configuration
{
    public class ProvincesSeedingConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasData(new Province { Id = 1, Name = "مازندران", CreatedAt = new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(780), UpdatedAt = new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(836) });
        }
    }
}
