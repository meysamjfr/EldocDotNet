using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Post;

namespace Project.Application.Features.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> Create(UpsertPost createPost);
        Task<DatatableResponse<PostDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<PostDTO> Edit(UpsertPost editPost);
        Task<List<PostDTO>> FilterPaginate(FilterPosts input);
        Task<List<PostDTO>> GetAll();
        Task<PostDTO> GetPost(int id);
        Task<UpsertPost> GetToEdit(int id);
    }
}
