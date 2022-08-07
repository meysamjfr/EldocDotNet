using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum FinancialContractType
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
