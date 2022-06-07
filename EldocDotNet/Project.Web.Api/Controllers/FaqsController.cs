using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.FAQ;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IFAQService _pageService;

        public FaqsController(IFAQService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        [Produces(typeof(Response<List<FAQDTO>>))]
        public async Task<JsonResult> GetAll()
        {
            var res = await _pageService.GetAll();

            return new Response<List<FAQDTO>>(res).ToJsonResult();
        }
    }
}
