using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Project.Application.Features.Interfaces;
using Project.Application.Extensions;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.DTOs.Datatable;
using Project.Application.Responses;
using System.ComponentModel;
using Project.Application.DTOs.Transaction;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("تراکنش‌های کاربران")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [DisplayName("لیست تراکنش‌های کاربران")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست تراکنش‌های کاربران";
            return View();
        }

        [DisplayName("ثبت تراکنش")]
        [HttpPost]
        public async Task<JsonResult> Upsert([FromForm] AddTransaction input)
        {
            return new Response<bool>(await _transactionService.AddTransactionByAdmin(input)).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(TransactionDatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _transactionService.Datatable(input, filters);
            return Json(res);
        }
    }
}
