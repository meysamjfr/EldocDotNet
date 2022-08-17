using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Expert;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository _expertRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IFileStorageService _storageService;
        private readonly string container;

        public ExpertService(IExpertRepository expertRepository,
                             IMapper mapper,
                             IUserRepository userRepository,
                             IFileStorageService storageService)
        {
            _expertRepository = expertRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _storageService = storageService;
            container = "experts";
        }

        public async Task<ExpertDTO> Create(UpsertExpert create)
        {
            if (await IsUsernameUnique(create.Username))
            {
                throw new BadRequestException("نام کاربری وارد شده تکراری است");
            }

            var model = _mapper.Map<Expert>(create);

            if (create.Image != null && create.Image.Length > 0)
            {
                model.ImageUrl = await _storageService.SaveFile(container, create.Image);
            }

            model = await _expertRepository.Add(model);

            return _mapper.Map<ExpertDTO>(model);
        }

        public async Task<UpsertExpert> GetToEdit(int id)
        {
            var find = await _expertRepository.GetNoTracking(id);

            return _mapper.Map<UpsertExpert>(find);
        }

        public async Task<ExpertDTO> Edit(UpsertExpert edit)
        {
            var find = await _expertRepository.GetNoTracking(edit.Id.Value);
            if (find == null)
            {
                throw new NotFoundException();
            }

            if (await IsUsernameUnique(edit.Username, find.Id))
            {
                throw new BadRequestException("نام کاربری وارد شده تکراری است");
            }

            var model = _mapper.Map<Expert>(edit);

            await _expertRepository.Update(model);

            return _mapper.Map<ExpertDTO>(model);
        }

        public async Task<DatatableResponse<ExpertDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _expertRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Name.Contains(filtersFromRequest.SearchValue) ||
                    w.Description.Contains(filtersFromRequest.SearchValue) ||
                    w.Specialty.Contains(filtersFromRequest.SearchValue)
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<Expert, ExpertDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<ExpertDTO>> GetAll()
        {
            var data = await _expertRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExpertDTO>>(data);
        }

        public async Task<List<ExpertCompact>> GetAllCompact()
        {
            var data = await _expertRepository.GetAllQueryable()
                .Where(w => w.IsActive == true)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExpertCompact>>(data);
        }

        private async Task<bool> IsUsernameUnique(string username, int currentUserId = 0)
        {
            var data = await _expertRepository.GetAllQueryable()
                .Where(w => w.Username == username)
                .AsNoTracking()
                .ToListAsync();

            if (currentUserId != 0)
            {
                data = data.Where(w => w.Id != currentUserId).ToList();
            }

            return data.Any();
        }

        public async Task Delete(int id)
        {
            if (await _expertRepository.Exist(id) == false)
            {
                throw new NotFoundException();
            }

            await _expertRepository.Delete(id);
        }
        public async Task Recover(int id)
        {
            if (await _expertRepository.Exist(id) == false)
            {
                throw new NotFoundException();
            }

            await _expertRepository.Recover(id);
        }

        public async Task<ExpertDTO> Login(ExpertLogin input)
        {
            var find = await _expertRepository.GetAllQueryable()
                .FirstOrDefaultAsync(f => f.Username == input.Username && f.Password == input.Password);

            if (find == null)
            {
                return null;
            }

            return _mapper.Map<ExpertDTO>(find);
        }
    }
}
