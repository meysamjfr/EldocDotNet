using Project.Application.DTOs.Page;

namespace Project.Application.Features.Interfaces
{
    public interface IPageService
    {
        Task<List<PageDTO>> GetAll();
        Task<PageDTO> GetPage(string uri);
    }
}
