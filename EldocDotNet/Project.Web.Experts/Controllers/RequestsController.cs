using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System.ComponentModel;

namespace Project.Web.Experts.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IChatWithExpertRequestService _chatWithExpertRequestService;

        public RequestsController(IChatWithExpertRequestService chatWithExpertRequestService)
        {
            _chatWithExpertRequestService = chatWithExpertRequestService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "لیست درخواست ها";

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(ChatWithExpertRequestDatatableInput input)
        {
            input.ExpertId = int.Parse(User.Identity.Name);
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _chatWithExpertRequestService.Datatable(input, filters);
            return Json(res);
        }

        [DisplayName("تایید درخواست")]
        public async Task<JsonResult> Accept(int id)
        {
            int _expertId = int.Parse(User.Identity.Name);
            await _chatWithExpertRequestService.AcceptRequest(id, _expertId);
            return Response<string>.Succeed();
        }

        [DisplayName("رد درخواست")]
        public async Task<JsonResult> Reject(int id)
        {
            int _expertId = int.Parse(User.Identity.Name);
            await _chatWithExpertRequestService.RejectRequest(id, _expertId);
            return Response<string>.Succeed();
        }
    }
}
