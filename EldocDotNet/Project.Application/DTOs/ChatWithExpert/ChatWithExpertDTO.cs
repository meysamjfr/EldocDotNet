using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.ChatWithExpert
{
    public class ChatWithExpertDTO : BaseDTO
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int ExpertId { get; set; }
        public string Expert { get; set; }
        public bool InProgress { get; set; }
        public int TotalMessages { get; set; } = 0;
        public int UnreadMessages { get; set; } = 0;
    }
}
