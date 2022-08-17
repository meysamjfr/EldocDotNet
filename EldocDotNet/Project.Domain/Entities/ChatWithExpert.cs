using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class ChatWithExpert : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
        public bool InProgress { get; set; }
        public ICollection<ChatWithExpertMessage> Messages { get; set; }
    }
}