using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class UnilateralContractTemplate : BaseEntity
    {
        public UnilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}