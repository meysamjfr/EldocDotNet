using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public long TrackingNumber { get; set; }
        public double Amount { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.Charge;
        public int? TypeActionId { get; set; }
        public string TransactionCode { get; set; }
        public string GatewayName { get; set; }
        public string GatewayAccountName { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}