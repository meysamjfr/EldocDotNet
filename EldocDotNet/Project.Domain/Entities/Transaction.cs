using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public string AdditionalData { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}