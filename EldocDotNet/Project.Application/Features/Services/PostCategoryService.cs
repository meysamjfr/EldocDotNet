using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.PostCategory;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IMapper mapper, IPostRepository postRepository)
        {
            _postCategoryRepository = postCategoryRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<DatatableResponse<PostCategoryDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _postCategoryRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Title.Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            return await data.ToDataTableAsync<PostCategory, PostCategoryDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<PostCategoryDTO>> GetAll()
        {
            var items = await _postCategoryRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .ToListAsync();

            return _mapper.Map<List<PostCategoryDTO>>(items);
        }

        public async Task<List<PostCategoryDTO>> GetAllPaginate(string search, int page)
        {
            var items = await _postCategoryRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.Title.Contains(search))
                .AsNoTracking()
                .PaginateDefault(page)
                .ToListAsync();

            return _mapper.Map<List<PostCategoryDTO>>(items);
        }
        public async Task<PostCategoryDTO> Create(UpsertPostCategory input)
        {
            var model = _mapper.Map<PostCategory>(input);

            model = await _postCategoryRepository.Add(model);

            return _mapper.Map<PostCategoryDTO>(model);
        }

        public async Task<PostCategoryDTO> Edit(UpsertPostCategory input)
        {
            var model = _mapper.Map<PostCategory>(input);
            model.Id = input.Id.Value;

            await _postCategoryRepository.Update(model);

            return _mapper.Map<PostCategoryDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            if (await _postRepository.GetAllQueryable().AnyAsync(a => a.PostCategoryId == id))
            {
                return false;
            }

            await _postCategoryRepository.Delete(id);

            return true;
        }
    }
}
