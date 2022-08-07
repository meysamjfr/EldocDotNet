using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("نمونه قرارداد دوطرفه")]
    public class BilateralContractTemplatesController : Controller
    {
        private readonly IBilateralContractTemplateService _bilateralContractTemplateService;

        public BilateralContractTemplatesController(IBilateralContractTemplateService bilateralContractTemplateService)
        {
            _bilateralContractTemplateService = bilateralContractTemplateService;
        }

        [DisplayName("لیست نمونه قرارداد دوطرفه")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست نمونه قرارداد دوطرفه";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(DatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _bilateralContractTemplateService.Datatable(input, filters);
            return Json(res);
        }

        [HttpPost]
        [DisplayName("ثبت و ویرایش نمونه قرارداد دوطرفه")]
        public async Task<JsonResult> Upsert(UpsertBilateralContractTemplate input)
        {
            if (input.Id.HasValue)
            {
                await _bilateralContractTemplateService.Edit(input);
            }
            else
            {
                await _bilateralContractTemplateService.Create(input);
            }

            return Response<string>.Succeed();
        }

        [DisplayName("حذف نمونه قرارداد دوطرفه")]
        public async Task<JsonResult> Delete(int id)
        {
            await _bilateralContractTemplateService.Delete(id);
            return Response<string>.Succeed();
        }
    }
}
