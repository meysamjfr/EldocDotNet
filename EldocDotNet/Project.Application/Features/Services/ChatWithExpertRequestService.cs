using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using Project.Domain.Enums;

namespace Project.Application.Features.Services
{
    public class ChatWithExpertRequestService : IChatWithExpertRequestService
    {
        private readonly IChatWithExpertRequestRepository _chatWithExpertRequestRepository;
        private readonly IMapper _mapper;
        private readonly UserDTO currentUser;
        private readonly IExpertRepository _expertRepository;
        private readonly IExpertService _expertService;

        public ChatWithExpertRequestService(IChatWithExpertRequestRepository chatWithExpertRequestRepository,
                                            IMapper mapper,
                                            IUserService userService,
                                            IExpertRepository expertRepository,
                                            IExpertService expertService)
        {
            _chatWithExpertRequestRepository = chatWithExpertRequestRepository;
            _mapper = mapper;
            currentUser = userService.Current();
            _expertRepository = expertRepository;
            _expertService = expertService;
        }

        public async Task<ChatWithExpertRequestDTO> CreateRequestByUser(int expertId)
        {
            var findExpert = await _expertRepository.GetNoTracking(expertId);
            if (findExpert == null)
            {
                throw new NotFoundException("کارشناس مورد نظر پیدا نشد");
            }

            var model = new ChatWithExpertRequest
            {
                ExpertId = expertId,
                UserId = currentUser.Id,
                Status = ChatWithExpertRequestStatus.Pending,
                Description = "",
                SessionFee = findExpert.SessionFee,
                IsPaid = false,
            };

            await _chatWithExpertRequestRepository.Add(model);

            return _mapper.Map<ChatWithExpertRequestDTO>(model);
        }

        public async Task<List<ChatWithExpertRequestDTO>> AllRequestsByUser()
        {
            var items = await _chatWithExpertRequestRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.UserId == currentUser.Id)
                .Include(i => i.User)
                .Include(i => i.Expert)
                .ToListAsync();

            return _mapper.Map<List<ChatWithExpertRequestDTO>>(items);
        }

        public async Task<bool> CancelRequestByUser(int requestId)
        {
            var find = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (find == null || find.UserId != currentUser.Id)
            {
                throw new NotFoundException();
            }

            find.Status = ChatWithExpertRequestStatus.Canceled;

            await _chatWithExpertRequestRepository.Update(find);

            return true;
        }

        public async Task<bool> AcceptRequest(int requestId, int expertId)
        {
            var find = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (find == null || find.ExpertId != expertId)
            {
                throw new NotFoundException();
            }

            find.Status = ChatWithExpertRequestStatus.Accepted;

            await _chatWithExpertRequestRepository.Update(find);

            return true;
        }

        public async Task<bool> RejectRequest(int requestId, int expertId)
        {
            var find = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (find == null || find.ExpertId != expertId)
            {
                throw new NotFoundException();
            }

            find.Status = ChatWithExpertRequestStatus.Rejected;

            await _chatWithExpertRequestRepository.Update(find);

            return true;
        }

        public async Task<bool> CompleteRequest(int requestId, int expertId)
        {
            var find = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (find == null || find.ExpertId != expertId)
            {
                throw new NotFoundException();
            }

            find.Status = ChatWithExpertRequestStatus.Completed;

            await _chatWithExpertRequestRepository.Update(find);

            return true;
        }

        public async Task<DatatableResponse<ChatWithExpertRequestDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _chatWithExpertRequestRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .Include(i => i.User)
                .Include(i => i.Expert)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {
                data = data.Where(w =>
                    w.Description.Contains(filtersFromRequest.SearchValue) ||
                    w.User.Nickname.Contains(filtersFromRequest.SearchValue) ||
                    w.Expert.Name.Contains(filtersFromRequest.SearchValue)
                );
            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<ChatWithExpertRequest, ChatWithExpertRequestDTO>(totalRecords, filtersFromRequest, _mapper);
        }

    }
}
