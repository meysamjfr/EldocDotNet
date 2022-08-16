using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.Gateway.ZarinPal;
using Project.Application.DTOs.ChatWithExpertRequest;
using Project.Application.DTOs.City;
using Project.Application.DTOs.Expert;
using Project.Application.DTOs.Payment;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Web.Api.Extensions;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UserAuthorize]
    public class ChatWithExpertRequestController : ControllerBase
    {
        private readonly IChatWithExpertRequestService _chatWithExpertRequestService;
        private readonly IExpertService _expertService;
        private readonly IPaymentService _paymentService;
        private readonly IOnlinePayment _onlinePayment;

        public ChatWithExpertRequestController(IChatWithExpertRequestService chatWithExpertRequestService, IExpertService expertService, IPaymentService paymentService, IOnlinePayment onlinePayment)
        {
            _chatWithExpertRequestService = chatWithExpertRequestService;
            _expertService = expertService;
            _paymentService = paymentService;
            _onlinePayment = onlinePayment;
        }

        [HttpPost("create")]
        [Produces(typeof(Response<ChatWithExpertRequestDTO>))]
        public async Task<JsonResult> Create([FromForm] int expertId)
        {
            var res = await _chatWithExpertRequestService.CreateRequestByUser(expertId);

            return new Response<ChatWithExpertRequestDTO>(res).ToJsonResult();

            //var callbackUrl = "https://localhost:44321/api/payment/verify";

            //IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
            //{
            //    invoice
            //        .SetZarinPalData($"پرداخت فاکتور - درخواست مذاکره با کارشناس")
            //        .SetTrackingNumber(DateTime.Now.Ticks)
            //        .SetAmount((decimal)res.SessionFee)
            //        .SetCallbackUrl(callbackUrl)
            //        .UseZarinPal();
            //});

            //if (result.IsSucceed)
            //{
            //    var addPayment = new AddPayment
            //    {
            //        AdditionalData = result.AdditionalData.ToString(),
            //        Amount = result.Amount,
            //        GatewayAccountName = result.GatewayAccountName,
            //        GatewayName = result.GatewayName,
            //        Message = result.Message,
            //        TrackingNumber = result.TrackingNumber,
            //        Token = $"chatwithexpertrequest-#{res.Id}",
            //        TransactionCode = Guid.NewGuid().ToString(),
            //    };

            //    await _paymentService.AddPayment(addPayment);

            //    return new Response<string>(result.GatewayTransporter.Descriptor.Url).ToJsonResult();
            //}

            //throw new BadRequestException($"خطای درگاه - {result.Message}");
        }

        [HttpGet("requests")]
        [Produces(typeof(Response<List<ChatWithExpertRequestDTO>>))]
        public async Task<JsonResult> GetAllRequests()
        {
            var res = await _chatWithExpertRequestService.AllRequestsByUser();

            return new Response<List<ChatWithExpertRequestDTO>>(res).ToJsonResult();
        }

        [HttpGet("experts")]
        [Produces(typeof(Response<List<ExpertCompact>>))]
        public async Task<JsonResult> GetAllExperts()
        {
            var res = await _expertService.GetAllCompact();

            return new Response<List<ExpertCompact>>(res).ToJsonResult();
        }

        [HttpPost("cancel")]
        [Produces(typeof(Response<bool>))]
        public async Task<JsonResult> Cancel([FromForm] int requestId)
        {
            var res = await _chatWithExpertRequestService.CancelRequestByUser(requestId);

            return new Response<bool>(res).ToJsonResult();
        }

    }
}
