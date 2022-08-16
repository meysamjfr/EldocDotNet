using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Gateway.ZarinPal;
using Project.Application.DTOs.Payment;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Web.Api.Extensions;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOnlinePayment _onlinePayment;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public PaymentController(IOnlinePayment onlinePayment, IPaymentService paymentService, IUserService userService)
        {
            _onlinePayment = onlinePayment;
            _paymentService = paymentService;
            _userService = userService;
        }

        [HttpPost("charge")]
        [Produces(typeof(Response<string>))]
        [UserAuthorize]
        public async Task<JsonResult> Charge([FromForm] decimal amount)
        {
            var callbackUrl = "https://localhost:44321/api/payment/verify";

            IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice
                    .SetZarinPalData($"شارژ حساب")
                    .SetTrackingNumber(DateTime.Now.Ticks)
                    .SetAmount(amount)
                    .SetCallbackUrl(callbackUrl)
                    .UseZarinPal();
            });

            if (result.IsSucceed)
            {
                await _paymentService.AddCharge(new AddPayment
                {
                    AdditionalData = result.AdditionalData.ToString(),
                    Amount = result.Amount,
                    GatewayAccountName = result.GatewayAccountName,
                    GatewayName = result.GatewayName,
                    Message = result.Message,
                    TrackingNumber = result.TrackingNumber,
                    TransactionCode = "",
                    UserId = _userService.Current().Id,
                });

                return new Response<string>(result.GatewayTransporter.Descriptor.Url).ToJsonResult();
            }

            throw new BadRequestException($"خطای درگاه - {result.Message}");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("verify")]
        public async Task<IActionResult> Verify()
        {
            IPaymentFetchResult invoice = await _onlinePayment.FetchAsync();

            switch (invoice.Status)
            {
                case PaymentFetchResultStatus.Failed:
                    return File("/payment-failed.html", "text/html");
                    //throw new BadRequestException($"خطا در دریافت اطلاعات پرداخت از درگاه - {invoice.Message}");

                case PaymentFetchResultStatus.AlreadyProcessed:
                    return File("/payment-succeed.html", "text/html");
                    //throw new BadRequestException("از قبل تایید شده بود");

                case PaymentFetchResultStatus.ReadyForVerifying:
                default:
                    await _paymentService.CheckTrackingNumber(invoice.TrackingNumber);
                    break;
            }

            IPaymentVerifyResult result = await _onlinePayment.VerifyAsync(invoice);

            switch (result.Status)
            {
                case PaymentVerifyResultStatus.Failed:
                    return File("/payment-failed.html", "text/html");
                    //throw new BadRequestException($"خطا در تایید پرداخت- {invoice.Message}");

                case PaymentVerifyResultStatus.AlreadyVerified:
                    return File("/payment-succeed.html", "text/html");
                    //throw new BadRequestException("از قبل تایید شده بود");

                case PaymentVerifyResultStatus.Succeed:
                    await _paymentService.VerifyPayment(result.TrackingNumber, result.TransactionCode);
                    break;
                default:
                    break;
            }

            return File("/payment-succeed.html", "text/html");
        }
    }
}
