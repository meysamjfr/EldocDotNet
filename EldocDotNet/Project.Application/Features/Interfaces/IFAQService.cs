using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FAQ;

namespace Project.Application.Features.Interfaces
{
    public interface IFAQService
    {
        Task<FAQDTO> Create(UpsertFAQ input);
        Task<DatatableResponse<FAQDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<FAQDTO> Edit(UpsertFAQ input);
        Task<List<FAQDTO>> GetAll();
        Task<FAQDTO> GetToEdit(int id);
        Task<bool> Recover(int id);
    }
}
