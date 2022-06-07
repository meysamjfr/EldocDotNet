using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<List<CityDTO>> SearchCitiesWithProvince(FilterCites filter)
        {
            var data = await _cityRepository
                .GetAllWithProvince()
                .Where(w =>
                    (!filter.ProvinceId.HasValue || w.ProvinceId == filter.ProvinceId) &&
                    (string.IsNullOrWhiteSpace(filter.Name.NormalizeText()) || w.Name.ToLower().Contains(filter.Name.NormalizeText())) &&
                    (string.IsNullOrWhiteSpace(filter.Name.NormalizeText()) || w.Province.Name.ToLower().Contains(filter.Name.NormalizeText()))
                    )
                .Paginate(filter)
                .ToListAsync();

            return _mapper.Map<List<CityDTO>>(data);
        }

        public async Task<DatatableResponse<CityDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _cityRepository.GetAllWithProvince(input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText()) ||
                    w.Province.Name.ToLower().Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            //if (!string.IsNullOrWhiteSpace(input.Name))
            //{
            //    data = data.Where(w =>
            //        w.Name.ToLower().Contains(input.Name.NormalizeText()) ||
            //        w.Province.Name.ToLower().Contains(input.Name.NormalizeText())
            //    );
            //}

            return await data.ToDataTableAsync<City, CityDTO>(totalRecords, filtersFromRequest, _mapper);
        }
    }
}
