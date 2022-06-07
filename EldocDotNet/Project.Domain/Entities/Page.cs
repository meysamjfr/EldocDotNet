using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class Page : BaseEntity
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
    }
}