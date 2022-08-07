using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.UnilateralContractTemplate;
using Project.Domain.Enums;

namespace Project.Application.Features.Interfaces
{
    public interface IUnilateralContractTemplateService
    {
        Task<UnilateralContractTemplateDTO> Create(UpsertUnilateralContractTemplate input);
        Task<DatatableResponse<UnilateralContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<UnilateralContractTemplateDTO> Edit(UpsertUnilateralContractTemplate input);
        Task<List<UnilateralContractTemplateDTO>> GetAll();
        Task<UnilateralContractTemplateDTO> GetTemplate(UnilateralContractType contractType);
        Task<UnilateralContractTemplateDTO> GetToEdit(int id);
        Task<bool> Recover(int id);
    }
}
