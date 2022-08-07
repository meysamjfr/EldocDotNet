using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class FinancialContractTemplateService : IFinancialContractTemplateService
    {
        private readonly IFinancialContractTemplateRepository _financialContractTemplateRepository;
        private readonly IMapper _mapper;

        public FinancialContractTemplateService(IFinancialContractTemplateRepository financialContractTemplateRepository, IMapper mapper)
        {
            _financialContractTemplateRepository = financialContractTemplateRepository;
            _mapper = mapper;
        }

        public async Task<DatatableResponse<FinancialContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _financialContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w => w.Content.Contains(filtersFromRequest.SearchValue.NormalizeText()));
            }

            return await data.ToDataTableAsync<FinancialContractTemplate, FinancialContractTemplateDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<FinancialContractTemplateDTO>> GetAll()
        {
            var items = await _financialContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<FinancialContractTemplateDTO>>(items);
        }

        public async Task<FinancialContractTemplateDTO> GetTemplate(FinancialContractType contractType)
        {
            var find = await _financialContractTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(w => w.IsActive == true && w.ContractType == contractType);

            if (find == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<FinancialContractTemplateDTO>(find);
        }

        public async Task<FinancialContractTemplateDTO> Create(UpsertFinancialContractTemplate input)
        {
            var model = _mapper.Map<FinancialContractTemplate>(input);

            model = await _financialContractTemplateRepository.Add(model);

            return _mapper.Map<FinancialContractTemplateDTO>(model);
        }

        public async Task<FinancialContractTemplateDTO> GetToEdit(int id)
        {
            var find = await _financialContractTemplateRepository.GetNoTracking(id);

            if (find == null)
            {
                return null;
            }

            return _mapper.Map<FinancialContractTemplateDTO>(find);
        }

        public async Task<FinancialContractTemplateDTO> Edit(UpsertFinancialContractTemplate input)
        {
            var model = _mapper.Map<FinancialContractTemplate>(input);
            model.Id = input.Id.Value;

            await _financialContractTemplateRepository.Update(model);

            return _mapper.Map<FinancialContractTemplateDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            await _financialContractTemplateRepository.Delete(id);

            return true;
        }

        public async Task<bool> Recover(int id)
        {
            await _financialContractTemplateRepository.Recover(id);

            return true;
        }
    }
}
