using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class ChatWithExpertMessage : BaseEntity
    {
        public int ChatWithExpertId { get; set; }
        public ChatWithExpert ChatWithExpert { get; set; }
        public string Content { get; set; }
        public bool IsUser { get; set; }
        public bool UserSeen { get; set; }
        public bool ExpertSeen { get; set; }
    }
}