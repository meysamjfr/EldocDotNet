using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Interfaces;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("داشبورد")]
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [DisplayName("صفحه اصلی")]
        public async Task<IActionResult> Index()
        {
            var data = await _dashboardService.Statistics();
            return View(data);
        }

        [HttpGet("/transactionschartdata")]
        public JsonResult TransactionsChartData()
        {
            var res = _dashboardService.TransactionsChart();
            return Json(res);
        }

        [HttpGet("/transactionspiechartdata")]
        public JsonResult TransactionsPieChartData()
        {
            var res = _dashboardService.TransactionsPieChart();
            return Json(res);
        }

        [HttpGet("/transactionschartpercitydata")]
        public JsonResult TransactionsChartPerCityData()
        {
            var res = _dashboardService.TransactionsChartPerCity();
            return Json(res);
        }
    }
}
