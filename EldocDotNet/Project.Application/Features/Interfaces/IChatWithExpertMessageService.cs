using Project.Application.DTOs.ChatWithExpertMessage;

namespace Project.Application.Features.Interfaces
{
    public interface IChatWithExpertMessageService
    {
        Task<ChatWithExpertMessageDTO> AddMessageByExpert(int chatWithExpertId, string content);
        Task<ChatWithExpertMessageDTO> AddMessageByUser(int chatWithExpertId, string content);
        Task<List<ChatWithExpertMessageDTO>> GetAllMessagesByExpert(int chatWithExpertId);
        Task<List<ChatWithExpertMessageDTO>> GetAllMessagesByUser(int chatWithExpertId);
        Task<bool> IsChatWithExpertAvailableForUser(int chatWithExpertId, bool inProgress = true);
        Task SeenMessagesByExpert(int chatWithExpertId);
        Task SeenMessagesByUser(int chatWithExpertId);
    }

}
