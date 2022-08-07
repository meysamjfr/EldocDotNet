using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.FinancialContractTemplate
{
    public class FinancialContractTemplateDTO : BaseDTO
    {
        public FinancialContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
