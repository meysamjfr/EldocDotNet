using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FAQ;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("سوالات متداول")]
    public class FAQsController : Controller
    {
        private readonly IFAQService _FAQService;

        public FAQsController(IFAQService FAQService)
        {
            _FAQService = FAQService;
        }

        [DisplayName("لیست سوالات متداول")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست سوالات متداول";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(DatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _FAQService.Datatable(input, filters);
            return Json(res);
        }

        [HttpPost]
        [DisplayName("ثبت و ویرایش سوالات متداول")]
        public async Task<JsonResult> Upsert(UpsertFAQ input)
        {
            if (input.Id.HasValue)
            {
                await _FAQService.Edit(input);
            }
            else
            {
                await _FAQService.Create(input);
            }

            return Response<string>.Succeed();
        }

        [DisplayName("حذف سوالات متداول")]
        public async Task<JsonResult> Delete(int id)
        {
            await _FAQService.Delete(id);
            return Response<string>.Succeed();
        }
    }
}
