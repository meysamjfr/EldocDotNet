using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.FAQ;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;

namespace Project.Application.Features.Services
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _FAQRepository;
        private readonly IMapper _mapper;

        public FAQService(IFAQRepository FAQRepository, IMapper mapper)
        {
            _FAQRepository = FAQRepository;
            _mapper = mapper;
        }

        public async Task<List<FAQDTO>> GetAll()
        {
            var items = await _FAQRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<FAQDTO>>(items);
        }
    }
}
