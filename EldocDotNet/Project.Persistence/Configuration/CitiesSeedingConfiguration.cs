using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;

namespace Project.Persistence.Configuration
{
    public class CitiesSeedingConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new City { Id = 1, Name = "ساری", ProvinceId = 1, CreatedAt = new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(3970), UpdatedAt = new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(3978) });
        }
    }
}
