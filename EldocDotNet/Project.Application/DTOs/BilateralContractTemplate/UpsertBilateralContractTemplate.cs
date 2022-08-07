using Project.Domain.Enums;

namespace Project.Application.DTOs.BilateralContractTemplate
{
    public class UpsertBilateralContractTemplate
    {
        public int? Id { get; set; }
        public BilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
