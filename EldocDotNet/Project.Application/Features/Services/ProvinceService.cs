using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Province;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;

        public ProvinceService(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        public async Task<DatatableResponse<ProvinceDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _provinceRepository.GetAllWithCity(input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            //if (!string.IsNullOrWhiteSpace(input.Name))
            //{
            //    data = data.Where(w =>
            //        w.Name.ToLower().Contains(input.Name.NormalizeText())
            //    );
            //}

            return await data.ToDataTableAsync<Province, ProvinceDTO>(totalRecords, filtersFromRequest, _mapper);
        }
    }
}
