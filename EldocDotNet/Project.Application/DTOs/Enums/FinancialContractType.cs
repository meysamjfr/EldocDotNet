using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Application.DTOs.Enums
{
    public enum FinancialContractTypeDTO
    {
        [EnumMember(Value = "فاکتور")]
        [Display(Name = "فاکتور")]
        Factor,

        [EnumMember(Value = "پیش فاکتور")]
        [Display(Name = "پیش فاکتور")]
        Invoice,

        [EnumMember(Value = "صورت حساب")]
        [Display(Name = "صورت حساب")]
        Bill,
    }
}
