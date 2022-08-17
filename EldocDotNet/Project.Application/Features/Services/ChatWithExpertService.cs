using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.ChatWithExpert;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class ChatWithExpertService : IChatWithExpertService
    {
        private readonly IMapper _mapper;
        private readonly IChatWithExpertRepository _chatWithExpertRepository;
        private readonly UserDTO currentUser;

        public ChatWithExpertService(IMapper mapper, IChatWithExpertRepository chatWithExpertRepository, IUserService userService)
        {
            _mapper = mapper;
            _chatWithExpertRepository = chatWithExpertRepository;
            currentUser = userService.Current();
        }

        public async Task<ChatWithExpertDTO> Create(int userId, int expertId)
        {
            var model = await _chatWithExpertRepository.Add(new ChatWithExpert
            {
                UserId = userId,
                ExpertId = expertId,
                InProgress = true
            });

            return _mapper.Map<ChatWithExpertDTO>(model);
        }

        public async Task Finish(int id)
        {
            var find = await _chatWithExpertRepository.GetNoTracking(id);

            if (find == null)
            {
                throw new NotFoundException();
            }

            find.InProgress = false;

            await _chatWithExpertRepository.Update(find);
        }

        public async Task<DatatableResponse<ChatWithExpertDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _chatWithExpertRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .Include(i => i.User)
                .Include(i => i.Expert)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Id.ToString().Contains(filtersFromRequest.SearchValue) ||
                    w.User.Nickname.Contains(filtersFromRequest.SearchValue) ||
                    w.Expert.Name.Contains(filtersFromRequest.SearchValue)
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<ChatWithExpert, ChatWithExpertDTO>(totalRecords, filtersFromRequest, _mapper);
        }

        public async Task<List<ChatWithExpertDTO>> GetAllPaginateByUser(string search, int page)
        {
            var items = await _chatWithExpertRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.Id.ToString().Contains(search))
                .Where(w => w.UserId == currentUser.Id)
                .PaginateDefault(page)
                .Include(i => i.User)
                .Include(i => i.Expert)
                .Include(i => i.Messages)
                .ToListAsync();

            return _mapper.Map<List<ChatWithExpertDTO>>(items);
        }
    }
}
