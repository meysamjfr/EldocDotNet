using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.TicketMessage;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("پشتیبانی")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [DisplayName("لیست تیکت ها")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست تیکت ها";
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string search)
        {
            var res = await _ticketService.GetAll(search, false);
            return Json(res);
        }

        [HttpGet]
        public async Task<JsonResult> Details(int id)
        {
            var res = await _ticketService.Details(id);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> GetData(TicketDatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _ticketService.Datatable(input, filters);
            return Json(res);
        }

        public async Task<JsonResult> Close(int id)
        {
            var res = await _ticketService.Close(id);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddMessage(CreateTicketMessage input)
        {
            var res = await _ticketService.AddMessage(input, true);
            return Json(res);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var res = await _ticketService.Delete(id);
            return Json(res);
        }
    }
}
