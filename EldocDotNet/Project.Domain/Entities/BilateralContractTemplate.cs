using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class BilateralContractTemplate : BaseEntity
    {
        public BilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}