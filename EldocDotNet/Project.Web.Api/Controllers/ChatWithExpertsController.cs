using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.ChatWithExpert;
using Project.Application.DTOs.ChatWithExpertMessage;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.Expert;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Web.Api.Extensions;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UserAuthorize]
    public class ChatWithExpertsController : ControllerBase
    {
        private readonly IChatWithExpertService _chatWithExpertService;
        private readonly IChatWithExpertMessageService _chatWithExpertMessageService;

        public ChatWithExpertsController(IChatWithExpertService chatWithExpertService, IChatWithExpertMessageService chatWithExpertMessageService)
        {
            _chatWithExpertService = chatWithExpertService;
            _chatWithExpertMessageService = chatWithExpertMessageService;
        }

        [HttpGet("paginate")]
        [Produces(typeof(Response<ChatWithExpertDTO>))]
        public async Task<JsonResult> Create(int page = 1, string search = "")
        {
            page = page < 1 ? 1 : page;
            search = string.IsNullOrWhiteSpace(search) ? "" : search;

            var res = await _chatWithExpertService.GetAllPaginateByUser(search, page);

            return new Response<List<ChatWithExpertDTO>>(res).ToJsonResult();
        }

        [HttpGet("messages")]
        [Produces(typeof(Response<List<ChatWithExpertMessageDTO>>))]
        public async Task<JsonResult> GetAllRequests(int chatWithExpertId)
        {
            var res = await _chatWithExpertMessageService.GetAllMessagesByUser(chatWithExpertId);

            await _chatWithExpertMessageService.SeenMessagesByUser(chatWithExpertId).ConfigureAwait(false);

            return new Response<List<ChatWithExpertMessageDTO>>(res).ToJsonResult();
        }

    }
}
