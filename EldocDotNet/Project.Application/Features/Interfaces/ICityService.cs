using Project.Application.DTOs.City;
using Project.Application.DTOs.Datatable.Base;

namespace Project.Application.Features.Interfaces
{
    public interface ICityService
    {
        Task<DatatableResponse<CityDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<List<CityDTO>> SearchCitiesWithProvince(FilterCites filter);
    }
}
