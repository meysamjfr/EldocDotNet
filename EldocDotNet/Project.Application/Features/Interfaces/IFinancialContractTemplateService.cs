using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Domain.Enums;

namespace Project.Application.Features.Interfaces
{
    public interface IFinancialContractTemplateService
    {
        Task<FinancialContractTemplateDTO> Create(UpsertFinancialContractTemplate input);
        Task<DatatableResponse<FinancialContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<FinancialContractTemplateDTO> Edit(UpsertFinancialContractTemplate input);
        Task<List<FinancialContractTemplateDTO>> GetAll();
        Task<FinancialContractTemplateDTO> GetTemplate(FinancialContractType contractType);
        Task<FinancialContractTemplateDTO> GetToEdit(int id);
        Task<bool> Recover(int id);
    }
}
