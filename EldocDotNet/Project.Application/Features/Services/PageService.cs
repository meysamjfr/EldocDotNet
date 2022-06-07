using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Page;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;

namespace Project.Application.Features.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public PageService(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<List<PageDTO>> GetAll()
        {
            var items = await _pageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<PageDTO>>(items);
        }

        public async Task<PageDTO> GetPage(string uri)
        {
            var find = await _pageRepository.GetAllQueryable()
                .FirstOrDefaultAsync(w => w.IsActive == true && w.Uri == uri);

            if (find == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<PageDTO>(find);
        }
    }
}
