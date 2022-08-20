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

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var chatUser = await _chatService.GetChatUser(Context.ConnectionId);
            ////await Clients.All.SendAsync("LogMessage", string.Format("{0} disconnected", chatUser.Name));
            //await _chatService.SetOffline(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageByExpert(int group, string message)
        {
            var res = await _chatWithExpertMessageService.AddMessageByExpert(group, message);

            await Clients.OthersInGroup(group.ToString()).SendAsync("ReceiveMessage", group, res);
            await Clients.Caller.SendAsync("ReceiveMyMessage", group, res);
        }


        public async Task SendMessage(int group, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", group, message);
        }

        public async Task Join(int group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());
            //var chatUser = await _chatService.GetChatUser(Context.ConnectionId);
            //await Clients.OthersInGroup(group).SendAsync("Join", chatUser.Name + " joined.");
        }

        public Task Leave(int group)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, group.ToString());
        }
    }
}
