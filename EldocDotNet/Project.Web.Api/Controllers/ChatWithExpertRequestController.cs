using Microsoft.AspNetCore.Mvc;
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
    public class ChatWithExpertRequestController : ControllerBase
    {
        private readonly IChatWithExpertRequestService _chatWithExpertRequestService;
        private readonly IExpertService _expertService;

        public ChatWithExpertRequestController(IChatWithExpertRequestService chatWithExpertRequestService, IExpertService expertService)
        {
            _chatWithExpertRequestService = chatWithExpertRequestService;
            _expertService = expertService;
        }

        [HttpPost("create")]
        [Produces(typeof(Response<ChatWithExpertRequestDTO>))]
        public async Task<JsonResult> Create([FromForm] int expertId)
        {
            var res = await _chatWithExpertRequestService.CreateRequestByUser(expertId);

            return new Response<ChatWithExpertRequestDTO>(res).ToJsonResult();
        }

        [HttpGet("requests")]
        [Produces(typeof(Response<List<ChatWithExpertRequestDTO>>))]
        public async Task<JsonResult> GetAllRequests()
        {
            var res = await _chatWithExpertRequestService.AllRequestsByUser();

            return new Response<List<ChatWithExpertRequestDTO>>(res).ToJsonResult();
        }

        [HttpGet("experts")]
        [Produces(typeof(Response<List<ExpertCompact>>))]
        public async Task<JsonResult> GetAllExperts()
        {
            var res = await _expertService.GetAllCompact();

            return new Response<List<ExpertCompact>>(res).ToJsonResult();
        }

        [HttpPost("pay-request")]
        [Produces(typeof(Response<bool>))]
        public async Task<JsonResult> PayRequest([FromForm] int requestId)
        {
            var res = await _chatWithExpertRequestService.PayRequestWithBalance(requestId);

            return new Response<bool>(res).ToJsonResult();
        }

        [HttpPost("cancel")]
        [Produces(typeof(Response<bool>))]
        public async Task<JsonResult> Cancel([FromForm] int requestId)
        {
            var res = await _chatWithExpertRequestService.CancelRequestByUser(requestId);

            return new Response<bool>(res).ToJsonResult();
        }

    }
}
