using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Description { get; set; }
    }
}