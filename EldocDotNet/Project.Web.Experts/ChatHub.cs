using Microsoft.AspNetCore.SignalR;
using Project.Application.Features.Interfaces;

namespace Project.Web.Experts
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IChatWithExpertMessageService _chatWithExpertMessageService;

        public ChatHub(IUserService userService, IChatWithExpertMessageService chatWithExpertMessageService)
        {
            _userService = userService;
            _chatWithExpertMessageService = chatWithExpertMessageService;
        }

        public override async Task OnConnectedAsync()
        {
            if (_userService.Current() != null)
            {
                await _userService.SetConnectionId(Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public async Task SendMessageByExpert(int group, string message)
        {
            var res = await _chatWithExpertMessageService.AddMessageByExpert(group, message);

            await Clients.OthersInGroup(group.ToString()).SendAsync("ReceiveMessage", group, res);
            await Clients.Caller.SendAsync("ReceiveMyMessage", group, res);
        }

        public async Task SendMessageByUser(int group, string message)
        {
            if (_userService.Current() != null)
            {
                var res = await _chatWithExpertMessageService.AddMessageByUser(group, message);
                await Clients.OthersInGroup(group.ToString()).SendAsync("ReceiveMessage", group, res);
                await Clients.Caller.SendAsync("ReceiveMyMessage", group, res);
            }
        }

        public async Task Join(int group)
        {
            if (_userService.Current() != null)
            {
                if (await _chatWithExpertMessageService.IsChatWithExpertAvailableForUser(group) == true)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());
                }
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());
            }
        }

        public Task Leave(int group)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, group.ToString());
        }
    }
}
