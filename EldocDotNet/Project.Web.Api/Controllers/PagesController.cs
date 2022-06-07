using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Page;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet("{uri}")]
        [Produces(typeof(Response<PageDTO>))]
        public async Task<JsonResult> Get(string uri)
        {
            var res = await _pageService.GetPage(uri);

            return new Response<PageDTO>(res).ToJsonResult();
        }
    }
}
