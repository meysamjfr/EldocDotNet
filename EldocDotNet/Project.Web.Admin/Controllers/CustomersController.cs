using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.Datatable.Base;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using System.ComponentModel;

namespace Project.Web.Admin.Controllers
{
    [Authorize(Policy = Models.ConstantPolicies.DynamicPermission)]
    [DisplayName("کاربران")]
    public class CustomersController : Controller
    {
        private readonly IUserService _userService;

        public CustomersController(IUserService userService)
        {
            _userService = userService;
        }

        [DisplayName("لیست کاربران")]
        public IActionResult Index()
        {
            ViewBag.Title = "لیست کاربران";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(UserDatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _userService.Datatable(input, filters);
            return Json(res);
        }
    }
}
