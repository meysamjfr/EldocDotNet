using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class ChatWithExpertRequest : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
        public ChatWithExpertRequestStatus Status { get; set; }
        public string Description { get; set; }
        public double SessionFee { get; set; }
    }
}