using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.City;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Produces(typeof(Response<List<CityDTO>>))]
        public async Task<JsonResult> Get([FromQuery] FilterCites filter)
        {
            var res = await _cityService.SearchCitiesWithProvince(filter);

            return new Response<List<CityDTO>>(res).ToJsonResult();
        }
    }
}
