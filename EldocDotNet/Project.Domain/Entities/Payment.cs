using Project.Domain.Entities.Base;

namespace Project.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public long TrackingNumber { get; set; }
        public decimal Amount { get; set; }
        public string Token { get; set; }
        public string TransactionCode { get; set; }
        public string GatewayName { get; set; }
        public string GatewayAccountName { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsPaid { get; set; } = false;
    }
}