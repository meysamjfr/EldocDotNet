using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Domain.Enums;

namespace Project.Application.Features.Interfaces
{
    public interface IBilateralContractTemplateService
    {
        Task<BilateralContractTemplateDTO> Create(UpsertBilateralContractTemplate input);
        Task<DatatableResponse<BilateralContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<BilateralContractTemplateDTO> Edit(UpsertBilateralContractTemplate input);
        Task<List<BilateralContractTemplateDTO>> GetAll();
        Task<BilateralContractTemplateDTO> GetTemplate(BilateralContractType contractType);
        Task<BilateralContractTemplateDTO> GetToEdit(int id);
        Task<bool> Recover(int id);
    }
}
