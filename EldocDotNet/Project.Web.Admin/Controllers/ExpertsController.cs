using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Project.Application.Features.Interfaces;
using Project.Application.Extensions;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Datatable;
using Project.Application.Responses;
using System.ComponentModel;
using Project.Application.DTOs.Expert;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("کارشناسان")]
    public class ExpertsController : Controller
    {
        private readonly IExpertService _expertService;

        public ExpertsController(IExpertService expertService)
        {
            _expertService = expertService;
        }

        [DisplayName("لیست کارشناسان")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست کارشناسان";
            return View();
        }

        [DisplayName("ثبت و ویرایش کارشناس")]
        public async Task<IActionResult> Upsert(int id)
        {
            ViewBag.Title = "ثبت کارشناس";
            if (id < 1)
            {
                return View();
            }

            var res = await _expertService.GetToEdit(id);

            if (res == null)
            {
                return View();
            }

            ViewBag.Title = $"ویرایش کارشناس: {res.Name}";

            return View(res);
        }

        [HttpPost]
        public async Task<JsonResult> Upsert([FromForm] UpsertExpert input)
        {
            if (input.Id > 0)
            {
                return new Response<ExpertDTO>(await _expertService.Edit(input)).ToJsonResult();
            }
            return new Response<ExpertDTO>(await _expertService.Create(input)).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(DatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _expertService.Datatable(input, filters);
            return Json(res);
        }

        [DisplayName("حذف کارشناس")]
        public async Task<JsonResult> Delete(int id)
        {
            await _expertService.Delete(id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
