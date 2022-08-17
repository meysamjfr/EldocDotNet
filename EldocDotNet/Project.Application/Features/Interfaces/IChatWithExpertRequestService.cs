using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.ChatWithExpertRequest;

namespace Project.Application.Features.Interfaces
{
    public interface IChatWithExpertRequestService
    {
        Task<bool> AcceptRequest(int requestId, int expertId);
        Task<ChatWithExpertRequestDTO> CreateRequestByUser(int expertId);
        Task<List<ChatWithExpertRequestDTO>> AllRequestsByUser();
        Task<bool> CancelRequestByUser(int requestId);
        Task<bool> CompleteRequest(int requestId, int expertId);
        Task<DatatableResponse<ChatWithExpertRequestDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> RejectRequest(int requestId, int expertId);
        Task<bool> PayRequestWithBalance(int requestId);
    }

}
