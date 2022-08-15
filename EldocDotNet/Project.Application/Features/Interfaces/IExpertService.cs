using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Expert;

namespace Project.Application.Features.Interfaces
{
    public interface IExpertService
    {
        Task<ExpertDTO> Create(UpsertExpert create);
        Task<DatatableResponse<ExpertDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task Delete(int id);
        Task<ExpertDTO> Edit(UpsertExpert edit);
        Task<List<ExpertDTO>> GetAllPaginate(string search, int page);
        Task<UpsertExpert> GetToEdit(int id);
        Task<ExpertDTO> Login(ExpertLogin input);
    }
}
