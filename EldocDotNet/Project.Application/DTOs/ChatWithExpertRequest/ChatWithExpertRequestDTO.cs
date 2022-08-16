using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.ChatWithExpertRequest
{
    public class ChatWithExpertRequestDTO : BaseDTO
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int ExpertId { get; set; }
        public string Expert { get; set; }
        public ChatWithExpertRequestStatus Status { get; set; }
        public string Description { get; set; }
        public double SessionFee { get; set; }
    }
}
