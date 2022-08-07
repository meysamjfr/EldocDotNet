using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class BilateralContract : ContractBase
    {
        public BilateralContractType ContractType { get; set; }
        public int? SecondUserId { get; set; }
        public User SecondUser { get; set; }
    }
}