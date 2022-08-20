using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.Datatable;

namespace Project.Application.Features.Interfaces
{
    public interface IChatWithExpertRequestService
    {
        Task<bool> AcceptRequest(int requestId, int expertId);
        Task<ChatWithExpertRequestDTO> CreateRequestByUser(int expertId);
        Task<List<ChatWithExpertRequestDTO>> AllRequestsByUser();
        Task<bool> CancelRequestByUser(int requestId);
        Task<bool> RejectRequest(int requestId, int expertId);
        Task<bool> PayRequestWithBalance(int requestId);
        Task<DatatableResponse<ChatWithExpertRequestDTO>> Datatable(ChatWithExpertRequestDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
    }

}
