using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.PostCategory;

namespace Project.Application.Features.Interfaces
{
    public interface IPostCategoryService
    {
        Task<PostCategoryDTO> Create(UpsertPostCategory input);
        Task<DatatableResponse<PostCategoryDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<PostCategoryDTO> Edit(UpsertPostCategory input);
        Task<List<PostCategoryDTO>> GetAll();
        Task<List<PostCategoryDTO>> GetAllPaginate(string search, int page);
    }
}
