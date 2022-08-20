using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;

namespace Project.Web.Experts.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatWithExpertService _chatWithExpertService;
        private readonly IChatWithExpertMessageService _chatWithExpertMessageService;

        public ChatController(IChatWithExpertService chatWithExpertService, IChatWithExpertMessageService chatWithExpertMessageService)
        {
            _chatWithExpertService = chatWithExpertService;
            _chatWithExpertMessageService = chatWithExpertMessageService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "اتاق مذاکره";

            return View();
        }

        public async Task<IActionResult> GetData(int page)
        {
            var expertId = int.Parse(User.Identity.Name);

            var items = await _chatWithExpertService.GetAllPaginate("", page < 1 ? 1 : page, expertId);

            return Json(items);
        }

        public async Task<IActionResult> GetMessages(int id)
        {
            var items = await _chatWithExpertMessageService.GetAllMessagesByExpert(id);

            return Json(items);
        }

        public async Task<IActionResult> SetRead(int id)
        {
            await _chatWithExpertMessageService.SeenMessagesByExpert(id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        public async Task<IActionResult> FinishChat(int id)
        {
            await _chatWithExpertService.Finish(id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
