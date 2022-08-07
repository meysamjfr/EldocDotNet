using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class UnilateralContract : ContractBase
    {
        public UnilateralContractType ContractType { get; set; }
    }
}