using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Gateway.ZarinPal;
using Project.Application.Responses;

namespace Project.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IOnlinePayment _onlinePayment;

        public PaymentController(IOnlinePayment onlinePayment)
        {
            _onlinePayment = onlinePayment;
        }

        [HttpGet("pay")]
        public async Task<JsonResult> Pay()
        {
            var callbackUrl = "https://localhost:44321/api/payment/verify";
            
            IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice
                    .SetZarinPalData($"پرداخت فاکتور - #1")
                    .SetTrackingNumber(DateTime.Now.Ticks)
                    .SetAmount(10000)
                    .SetCallbackUrl(callbackUrl)
                    .UseZarinPal();
            });

            return new Response<IPaymentRequestResult>(result).ToJsonResult();
        }

        [HttpGet("verify")]
        public async Task<JsonResult> Verify()
        {
            IPaymentFetchResult invoice = await _onlinePayment.FetchAsync();

            IPaymentVerifyResult result = await _onlinePayment.VerifyAsync(invoice);

            if(result.Status == PaymentVerifyResultStatus.Failed)
            {

            }

            return new Response<IPaymentVerifyResult>(result).ToJsonResult();
        }
    }
}
