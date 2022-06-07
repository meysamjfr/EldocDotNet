using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }
        public string Content { get; set; }
    }
}