using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
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
        private readonly IUserService _userService;
        private readonly IExpertRepository _expertRepository;
        private readonly ITransactionService _transactionService;
        private readonly IChatWithExpertService _chatWithExpertService;

        public ChatWithExpertRequestService(IChatWithExpertRequestRepository chatWithExpertRequestRepository,
                                            IMapper mapper,
                                            IUserService userService,
                                            IExpertRepository expertRepository,
                                            ITransactionService transactionService,
                                            IChatWithExpertService chatWithExpertService)
        {
            _chatWithExpertRequestRepository = chatWithExpertRequestRepository;
            _mapper = mapper;
            _userService = userService;
            _expertRepository = expertRepository;
            _transactionService = transactionService;
            _chatWithExpertService = chatWithExpertService;
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
                UserId = _userService.Current().Id,
                Status = ChatWithExpertRequestStatus.Pending,
                Description = "",
                SessionFee = findExpert.SessionFee,
                IsPaid = false,
            };

            if (await _userService.CheckBalance(model.SessionFee))
            {
                var newBalance = await _transactionService.PayChatWithExpertRequest(_userService.Current().Id, model.SessionFee, $"پرداخت از اعتبار حساب");
                await _userService.UpdateBalance(newBalance);
                model.IsPaid = true;
            }

            await _chatWithExpertRequestRepository.Add(model);

            return _mapper.Map<ChatWithExpertRequestDTO>(model);
        }

        public async Task<bool> PayRequestWithBalance(int requestId)
        {
            var findRequest = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (findRequest == null)
            {
                throw new NotFoundException();
            }

            if (await _userService.CheckBalance(findRequest.SessionFee))
            {
                var newBalance = await _transactionService.PayChatWithExpertRequest(_userService.Current().Id, findRequest.SessionFee, $"پرداخت از اعتبار حساب");
                await _userService.UpdateBalance(newBalance);
                findRequest.IsPaid = true;
                await _chatWithExpertRequestRepository.Update(findRequest);

                return true;
            }

            throw new BadRequestException("اعتبار حساب کافی نیست");
        }

        public async Task<List<ChatWithExpertRequestDTO>> AllRequestsByUser()
        {
            var items = await _chatWithExpertRequestRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.UserId == _userService.Current().Id)
                .Include(i => i.User)
                .Include(i => i.Expert)
                .ToListAsync();

            return _mapper.Map<List<ChatWithExpertRequestDTO>>(items);
        }

        public async Task<bool> CancelRequestByUser(int requestId)
        {
            var find = await _chatWithExpertRequestRepository.GetNoTracking(requestId);

            if (find == null || find.UserId != _userService.Current().Id)
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

            await _chatWithExpertService.Create(find.UserId, find.ExpertId);

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

        public async Task<DatatableResponse<ChatWithExpertRequestDTO>> Datatable(ChatWithExpertRequestDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _chatWithExpertRequestRepository.GetAllQueryable()
                .Where(w => w.IsActive == input.IsActive)
                .Where(w => !input.ExpertId.HasValue || w.ExpertId == input.ExpertId)
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
