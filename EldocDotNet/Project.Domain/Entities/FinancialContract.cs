using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class FinancialContract : ContractBase
    {
        public FinancialContractType ContractType { get; set; }
    }
}