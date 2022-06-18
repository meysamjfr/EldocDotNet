using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class PostCategory : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

}