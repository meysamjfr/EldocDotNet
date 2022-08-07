using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("نمونه قرارداد مالی")]
    public class FinancialContractTemplatesController : Controller
    {
        private readonly IFinancialContractTemplateService _financialContractTemplateService;

        public FinancialContractTemplatesController(IFinancialContractTemplateService financialContractTemplateService)
        {
            _financialContractTemplateService = financialContractTemplateService;
        }

        [DisplayName("لیست نمونه قرارداد مالی")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست نمونه قرارداد مالی";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(DatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _financialContractTemplateService.Datatable(input, filters);
            return Json(res);
        }

        [HttpPost]
        [DisplayName("ثبت و ویرایش نمونه قرارداد مالی")]
        public async Task<JsonResult> Upsert(UpsertFinancialContractTemplate input)
        {
            if (input.Id.HasValue)
            {
                await _financialContractTemplateService.Edit(input);
            }
            else
            {
                await _financialContractTemplateService.Create(input);
            }

            return Response<string>.Succeed();
        }

        [DisplayName("حذف نمونه قرارداد مالی")]
        public async Task<JsonResult> Delete(int id)
        {
            await _financialContractTemplateService.Delete(id);
            return Response<string>.Succeed();
        }
    }
}
