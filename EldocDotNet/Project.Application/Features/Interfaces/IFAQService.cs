using Project.Application.DTOs.FAQ;

namespace Project.Application.Features.Interfaces
{
    public interface IFAQService
    {
        Task<List<FAQDTO>> GetAll();
    }
}
