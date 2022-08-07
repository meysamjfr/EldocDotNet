using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class FinancialContractTemplate : BaseEntity
    {
        public FinancialContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}