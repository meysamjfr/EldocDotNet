using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FAQ;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

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

        public async Task<DatatableResponse<FAQDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _FAQRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Content.Contains(filtersFromRequest.SearchValue.NormalizeText()) ||
                    w.Question.Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            return await data.ToDataTableAsync<FAQ, FAQDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<FAQDTO>> GetAll()
        {
            var items = await _FAQRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<FAQDTO>>(items);
        }

        public async Task<FAQDTO> Create(UpsertFAQ input)
        {
            var model = _mapper.Map<FAQ>(input);

            model = await _FAQRepository.Add(model);

            return _mapper.Map<FAQDTO>(model);
        }

        public async Task<FAQDTO> GetToEdit(int id)
        {
            var find = await _FAQRepository.GetNoTracking(id);

            if (find == null)
            {
                return null;
            }

            return _mapper.Map<FAQDTO>(find);
        }

        public async Task<FAQDTO> Edit(UpsertFAQ input)
        {
            var model = _mapper.Map<FAQ>(input);
            model.Id = input.Id.Value;

            await _FAQRepository.Update(model);

            return _mapper.Map<FAQDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            await _FAQRepository.Delete(id);

            return true;
        }

        public async Task<bool> Recover(int id)
        {
            await _FAQRepository.Recover(id);

            return true;
        }
    }
}
