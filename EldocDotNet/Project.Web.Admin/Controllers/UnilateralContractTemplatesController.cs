using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.UnilateralContractTemplate;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("نمونه قرارداد یک‌طرفه")]
    public class UnilateralContractTemplatesController : Controller
    {
        private readonly IUnilateralContractTemplateService _unilateralContractTemplateService;

        public UnilateralContractTemplatesController(IUnilateralContractTemplateService unilateralContractTemplateService)
        {
            _unilateralContractTemplateService = unilateralContractTemplateService;
        }

        [DisplayName("لیست نمونه قرارداد یک‌طرفه")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست نمونه قرارداد یک‌طرفه";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(DatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _unilateralContractTemplateService.Datatable(input, filters);
            return Json(res);
        }

        [HttpPost]
        [DisplayName("ثبت و ویرایش نمونه قرارداد یک‌طرفه")]
        public async Task<JsonResult> Upsert(UpsertUnilateralContractTemplate input)
        {
            if (input.Id.HasValue)
            {
                await _unilateralContractTemplateService.Edit(input);
            }
            else
            {
                await _unilateralContractTemplateService.Create(input);
            }

            return Response<string>.Succeed();
        }

        [DisplayName("حذف نمونه قرارداد یک‌طرفه")]
        public async Task<JsonResult> Delete(int id)
        {
            await _unilateralContractTemplateService.Delete(id);
            return Response<string>.Succeed();
        }
    }
}
