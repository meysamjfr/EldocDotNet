using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Province;

namespace Project.Application.Features.Interfaces
{
    public interface IProvinceService
    {
        Task<DatatableResponse<ProvinceDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
    }
}
