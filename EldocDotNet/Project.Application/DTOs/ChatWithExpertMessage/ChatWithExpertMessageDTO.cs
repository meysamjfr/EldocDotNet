using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.ChatWithExpertMessage
{
    public class ChatWithExpertMessageDTO : BaseDTO
    {
        public string Content { get; set; }
        public bool IsUser { get; set; }
        public bool UserSeen { get; set; }
        public bool ExpertSeen { get; set; }
    }
}
