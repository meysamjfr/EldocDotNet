using Project.Domain.Enums;

namespace Project.Application.DTOs.FinancialContractTemplate
{
    public class UpsertFinancialContractTemplate
    {
        public int? Id { get; set; }
        public FinancialContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
