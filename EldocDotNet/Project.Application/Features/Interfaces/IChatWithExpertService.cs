using Project.Application.DTOs.ChatWithExpert;
using Project.Application.DTOs.Datatable.Base;

namespace Project.Application.Features.Interfaces
{
    public interface IChatWithExpertService
    {
        Task<ChatWithExpertDTO> Create(int userId, int expertId);
        Task<DatatableResponse<ChatWithExpertDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task Finish(int id);
        Task<List<ChatWithExpertDTO>> GetAllPaginate(string search, int page, int? expertId);
        Task<List<ChatWithExpertDTO>> GetAllPaginateByUser(string search, int page);
    }

}
