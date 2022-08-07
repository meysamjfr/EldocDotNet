using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class BilateralContractTemplateService : IBilateralContractTemplateService
    {
        private readonly IBilateralContractTemplateRepository _bilateralContractTemplateRepository;
        private readonly IMapper _mapper;

        public BilateralContractTemplateService(IBilateralContractTemplateRepository bilateralContractTemplateRepository, IMapper mapper)
        {
            _bilateralContractTemplateRepository = bilateralContractTemplateRepository;
            _mapper = mapper;
        }

        public async Task<DatatableResponse<BilateralContractTemplateDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _bilateralContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w => w.Content.Contains(filtersFromRequest.SearchValue.NormalizeText()));
            }

            return await data.ToDataTableAsync<BilateralContractTemplate, BilateralContractTemplateDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<BilateralContractTemplateDTO>> GetAll()
        {
            var items = await _bilateralContractTemplateRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<BilateralContractTemplateDTO>>(items);
        }

        public async Task<BilateralContractTemplateDTO> GetTemplate(BilateralContractType contractType)
        {
            var find = await _bilateralContractTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(w => w.IsActive == true && w.ContractType == contractType);

            if (find == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<BilateralContractTemplateDTO>(find);
        }

        public async Task<BilateralContractTemplateDTO> Create(UpsertBilateralContractTemplate input)
        {
            var model = _mapper.Map<BilateralContractTemplate>(input);

            model = await _bilateralContractTemplateRepository.Add(model);

            return _mapper.Map<BilateralContractTemplateDTO>(model);
        }

        public async Task<BilateralContractTemplateDTO> GetToEdit(int id)
        {
            var find = await _bilateralContractTemplateRepository.GetNoTracking(id);

            if (find == null)
            {
                return null;
            }

            return _mapper.Map<BilateralContractTemplateDTO>(find);
        }

        public async Task<BilateralContractTemplateDTO> Edit(UpsertBilateralContractTemplate input)
        {
            var model = _mapper.Map<BilateralContractTemplate>(input);
            model.Id = input.Id.Value;

            await _bilateralContractTemplateRepository.Update(model);

            return _mapper.Map<BilateralContractTemplateDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            await _bilateralContractTemplateRepository.Delete(id);

            return true;
        }

        public async Task<bool> Recover(int id)
        {
            await _bilateralContractTemplateRepository.Recover(id);

            return true;
        }
    }
}
