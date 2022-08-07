using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.UnilateralContractTemplate;
using Project.Application.DTOs.BilateralContractTemplate;
using Project.Application.DTOs.FinancialContractTemplate;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Domain.Enums;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTemplatesController : ControllerBase
    {
        private readonly IUnilateralContractTemplateService _unilateralContractTemplateService;
        private readonly IBilateralContractTemplateService _bilateralContractTemplateService;
        private readonly IFinancialContractTemplateService _financialContractTemplateService;

        public ContractTemplatesController(IUnilateralContractTemplateService unilateralContractTemplateService, IBilateralContractTemplateService bilateralContractTemplateService, IFinancialContractTemplateService financialContractTemplateService)
        {
            _unilateralContractTemplateService = unilateralContractTemplateService;
            _bilateralContractTemplateService = bilateralContractTemplateService;
            _financialContractTemplateService = financialContractTemplateService;
        }

        [HttpGet("[action]")]
        [Produces(typeof(Response<UnilateralContractTemplateDTO>))]
        public async Task<JsonResult> Unilateral([FromQuery] UnilateralContractType contractType)
        {
            var res = await _unilateralContractTemplateService.GetTemplate(contractType);

            return new Response<UnilateralContractTemplateDTO>(res).ToJsonResult();
        }

        [HttpGet("[action]")]
        [Produces(typeof(Response<BilateralContractTemplateDTO>))]
        public async Task<JsonResult> Bilateral([FromQuery] BilateralContractType contractType)
        {
            var res = await _bilateralContractTemplateService.GetTemplate(contractType);

            return new Response<BilateralContractTemplateDTO>(res).ToJsonResult();
        }

        [HttpGet("[action]")]
        [Produces(typeof(Response<FinancialContractTemplateDTO>))]
        public async Task<JsonResult> Financial([FromQuery] FinancialContractType contractType)
        {
            var res = await _financialContractTemplateService.GetTemplate(contractType);

            return new Response<FinancialContractTemplateDTO>(res).ToJsonResult();
        }
    }
}
