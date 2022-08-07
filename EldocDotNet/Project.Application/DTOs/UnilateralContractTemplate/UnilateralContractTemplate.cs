using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.UnilateralContractTemplate
{
    public class UnilateralContractTemplateDTO : BaseDTO
    {
        public UnilateralContractType ContractType { get; set; }
        public string Content { get; set; }
    }
}
