using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.UnilateralContractTemplate;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class UnilateralContractTemplateService : IUnilateralContractTemplateService
    {
        private readonly IUnilateralContractTemplateRepository _unilateralContractTemplateRepository;
        private readonly IMapper _mapper;

        public UnilateralContractTemplateService(IUnilateralContractTemplateRepository unilateralContractTemplateRepository, IMapper mapper)
        {
            _unilateralContractTemplateRepository = unilateralContractTemplateRepository;
            _mapper = mapper;
        }

        public async Task<DatatableResponse<UnilateralContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _unilateralContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w => w.Content.Contains(filtersFromRequest.SearchValue.NormalizeText()));
            }

            return await data.ToDataTableAsync<UnilateralContractTemplate, UnilateralContractTemplateDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<UnilateralContractTemplateDTO>> GetAll()
        {
            var items = await _unilateralContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<UnilateralContractTemplateDTO>>(items);
        }

        public async Task<UnilateralContractTemplateDTO> GetTemplate(UnilateralContractType contractType)
        {
            var find = await _unilateralContractTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(w => w.IsActive == true && w.ContractType == contractType);

            if (find == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UnilateralContractTemplateDTO>(find);
        }

        public async Task<UnilateralContractTemplateDTO> Create(UpsertUnilateralContractTemplate input)
        {
            var model = _mapper.Map<UnilateralContractTemplate>(input);

            model = await _unilateralContractTemplateRepository.Add(model);

            return _mapper.Map<UnilateralContractTemplateDTO>(model);
        }

        public async Task<UnilateralContractTemplateDTO> GetToEdit(int id)
        {
            var find = await _unilateralContractTemplateRepository.GetNoTracking(id);

            if (find == null)
            {
                return null;
            }

            return _mapper.Map<UnilateralContractTemplateDTO>(find);
        }

        public async Task<UnilateralContractTemplateDTO> Edit(UpsertUnilateralContractTemplate input)
        {
            var model = _mapper.Map<UnilateralContractTemplate>(input);
            model.Id = input.Id.Value;

            await _unilateralContractTemplateRepository.Update(model);

            return _mapper.Map<UnilateralContractTemplateDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            await _unilateralContractTemplateRepository.Delete(id);

            return true;
        }

        public async Task<bool> Recover(int id)
        {
            await _unilateralContractTemplateRepository.Recover(id);

            return true;
        }
    }
}
