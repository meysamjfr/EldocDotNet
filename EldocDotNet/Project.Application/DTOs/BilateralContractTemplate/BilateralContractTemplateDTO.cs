using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.BilateralContractTemplate
{
    public class BilateralContractTemplateDTO : BaseDTO
    {
        public BilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
