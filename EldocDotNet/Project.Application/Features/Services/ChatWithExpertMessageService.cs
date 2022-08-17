using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.ChatWithExpertMessage;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Features.Services
{
    public class ChatWithExpertMessageService : IChatWithExpertMessageService
    {
        private readonly IMapper _mapper;
        private readonly IChatWithExpertMessageRepository _chatWithExpertMessageRepository;
        private readonly UserDTO currentUser;
        private readonly IChatWithExpertRepository _chatWithExpertRepository;

        public ChatWithExpertMessageService(IMapper mapper, IChatWithExpertMessageRepository chatWithExpertMessageRepository, IUserService userService, IChatWithExpertRepository chatWithExpertRepository)
        {
            _mapper = mapper;
            _chatWithExpertMessageRepository = chatWithExpertMessageRepository;
            currentUser = userService.Current();
            _chatWithExpertRepository = chatWithExpertRepository;
        }

        private async Task<bool> IsChatWithExpertAvailableForUser(int chatWithExpertId)
        {
            return await _chatWithExpertRepository.GetAllQueryable().AnyAsync(a => a.IsActive == true
                                                                                   && a.InProgress == true
                                                                                   && a.UserId == currentUser.Id
                                                                                   && a.Id == chatWithExpertId);
        }

        public async Task<ChatWithExpertMessageDTO> AddMessageByUser(int chatWithExpertId, string content)
        {
            if (await IsChatWithExpertAvailableForUser(chatWithExpertId) == false)
            {
                throw new NotFoundException();
            }

            var model = await _chatWithExpertMessageRepository.Add(new ChatWithExpertMessage
            {
                Content = content,
                ChatWithExpertId = chatWithExpertId,
                IsUser = true,
                ExpertSeen = false,
                UserSeen = true
            });

            return _mapper.Map<ChatWithExpertMessageDTO>(model);
        }

        public async Task<ChatWithExpertMessageDTO> AddMessageByExpert(int chatWithExpertId, string content)
        {

            var model = await _chatWithExpertMessageRepository.Add(new ChatWithExpertMessage
            {
                Content = content,
                ChatWithExpertId = chatWithExpertId,
                IsUser = false,
                ExpertSeen = true,
                UserSeen = false
            });

            return _mapper.Map<ChatWithExpertMessageDTO>(model);
        }

        public async Task<List<ChatWithExpertMessageDTO>> GetAllMessagesByUser(int chatWithExpertId)
        {
            if (await IsChatWithExpertAvailableForUser(chatWithExpertId) == false)
            {
                throw new NotFoundException();
            }

            var items = await _chatWithExpertMessageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.ChatWithExpertId == chatWithExpertId)
                .ToListAsync();

            return _mapper.Map<List<ChatWithExpertMessageDTO>>(items);
        }

        public async Task<List<ChatWithExpertMessageDTO>> GetAllMessagesByExpert(int chatWithExpertId)
        {
            var items = await _chatWithExpertMessageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.ChatWithExpertId == chatWithExpertId)
                .ToListAsync();

            return _mapper.Map<List<ChatWithExpertMessageDTO>>(items);
        }

        public async Task SeenMessagesByUser(int chatWithExpertId)
        {
            if (await IsChatWithExpertAvailableForUser(chatWithExpertId) == false)
            {
                throw new NotFoundException();
            }

            var items = await _chatWithExpertMessageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.ChatWithExpertId == chatWithExpertId && w.UserSeen == false)
                .ToListAsync();

            foreach (var item in items)
            {
                item.UserSeen = true;
                await _chatWithExpertMessageRepository.Update(item);
            }
        }

        public async Task SeenMessagesByExpert(int chatWithExpertId)
        {
            var items = await _chatWithExpertMessageRepository.GetAllQueryable()
                .Where(w => w.IsActive == true && w.ChatWithExpertId == chatWithExpertId && w.ExpertSeen == false)
                .ToListAsync();

            foreach (var item in items)
            {
                item.ExpertSeen = true;
                await _chatWithExpertMessageRepository.Update(item);
            }
        }
    }
}
