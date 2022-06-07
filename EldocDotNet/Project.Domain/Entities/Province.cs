using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}