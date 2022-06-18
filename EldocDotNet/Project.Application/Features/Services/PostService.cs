using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Post;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly string container;
        private readonly IFileStorageService _storageService;

        public PostService(IPostRepository postRepository, IMapper mapper, IFileStorageService storageService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            container = "posts";
            _storageService = storageService;
        }

        public async Task<DatatableResponse<PostDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _postRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Content.Contains(filtersFromRequest.SearchValue.NormalizeText()) ||
                    w.Title.Contains(filtersFromRequest.SearchValue.NormalizeText())
                );
            }

            return await data.ToDataTableAsync<Post, PostDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<PostDTO>> GetAll()
        {
            var items = await _postRepository.GetAllQueryable()
                                             .Where(w => w.IsActive == true)
                                             .ToListAsync();

            return _mapper.Map<List<PostDTO>>(items);
        }

        public async Task<List<PostDTO>> FilterPaginate(FilterPosts input)
        {
            var data = _postRepository.GetAllQueryable()
                                      .Where(w => w.IsActive == true);

            if (input.CategoryId.HasValue && input.CategoryId > 0)
            {
                data = data.Where(w => w.PostCategoryId == input.CategoryId.Value);
            }

            if (string.IsNullOrWhiteSpace(input.Search))
            {
                data = data.Where(w => w.Title.Contains(input.Search)
                                       || w.Tags.Contains(input.Search)
                                       || w.Content.Contains(input.Search));
            }

            var res = await data.OrderByDescending(o => o.UpdatedAt)
                                .Paginate(input)
                                .ToListAsync();

            return _mapper.Map<List<PostDTO>>(res);
        }

        public async Task<PostDTO> GetPost(int id)
        {
            var find = await _postRepository.GetNoTracking(id);

            if (find == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<PostDTO>(find);
        }

        public async Task<PostDTO> Create(UpsertPost createPost)
        {
            if (createPost.Image == null)
            {
                throw new ValidationException("تصویر خبر را وارد نکرده اید");
            }

            var model = _mapper.Map<Post>(createPost);

            model.ImageUrl = await _storageService.SaveFile(container, createPost.Image);

            model = await _postRepository.Add(model);

            return _mapper.Map<PostDTO>(model);
        }

        public async Task<UpsertPost> GetToEdit(int id)
        {
            var find = await _postRepository.GetNoTracking(id);

            return _mapper.Map<UpsertPost>(find);
        }

        public async Task<PostDTO> Edit(UpsertPost editPost)
        {
            var find = await _postRepository.GetNoTracking(editPost.Id.Value);
            if (find == null)
            {
                throw new NotFoundException();
            }

            var model = _mapper.Map<Post>(editPost);

            if (editPost.Image != null)
            {
                model.ImageUrl = await _storageService.EditFile(container, editPost.Image, find.ImageUrl);
            }

            await _postRepository.Update(model);

            return _mapper.Map<PostDTO>(model);
        }

        public async Task<bool> Delete(int id)
        {
            await _postRepository.Delete(id);

            return true;
        }
    }
}
