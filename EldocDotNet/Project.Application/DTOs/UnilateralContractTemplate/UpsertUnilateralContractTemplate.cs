using Project.Domain.Enums;

namespace Project.Application.DTOs.UnilateralContractTemplate
{
    public class UpsertUnilateralContractTemplate
    {
        public int? Id { get; set; }
        public UnilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
