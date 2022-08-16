using Project.Domain.Enums;

namespace Project.Application.DTOs.Payment
{
    public class AddPayment
    {
        public long TrackingNumber { get; set; }
        public decimal Amount { get; set; }
        public string GatewayName { get; set; }
        public string GatewayAccountName { get; set; }
        public string Message { get; set; }
        public string AdditionalData { get; set; }
        public string TransactionCode { get; set; }
        public int UserId { get; set; }
        public string PaymentUrl { get; set; }
    }
}
