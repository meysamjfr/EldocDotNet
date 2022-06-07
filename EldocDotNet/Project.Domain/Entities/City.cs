using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
    }
}