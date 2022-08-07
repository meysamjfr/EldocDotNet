using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum BilateralContractType
    {
        [EnumMember(Value = "بیع - خرید / فروش")]
        [Display(Name = "بیع - خرید / فروش")]
        CreditSale,

        [EnumMember(Value = "تعهد به بیع")]
        [Display(Name = "تعهد به بیع")]
        CreditSaleObligation,

        [EnumMember(Value = "رهن و وثیقه - غیربانکی")]
        [Display(Name = "رهن و وثیقه - غیربانکی")]
        MortgageAndCollateral,

        [EnumMember(Value = "اجاره - تملیک منافع")]
        [Display(Name = "اجاره - تملیک منافع")]
        Rent,

        [EnumMember(Value = "مشارکت مدنی - ساخت و ساز")]
        [Display(Name = "مشارکت مدنی - ساخت و ساز")]
        CivilPartnership,

        [EnumMember(Value = "مشارکت - شراکت نامه")]
        [Display(Name = "مشارکت - شراکت نامه")]
        Partnership,

        [EnumMember(Value = "تقسیم نامه")]
        [Display(Name = "تقسیم نامه")]
        LetterOfDivision,

        [EnumMember(Value = "وصیت نامه - عهدی / تملیکی")]
        [Display(Name = "وصیت نامه - عهدی / تملیکی")]
        Will,

        [EnumMember(Value = "صلح - در مقام بیع")]
        [Display(Name = "صلح - در مقام بیع")]
        Peace,

        [EnumMember(Value = "صلح و سازش - ترک دعوی")]
        [Display(Name = "صلح و سازش - ترک دعوی")]
        PeaceAndReconciliation,

        [EnumMember(Value = "هبه")]
        [Display(Name = "هبه")]
        Gift,

        [EnumMember(Value = "جعاله")]
        [Display(Name = "جعاله")]
        Reward,

        [EnumMember(Value = "خرید خدمات - استخدام / مشاوره / نمایندگی")]
        [Display(Name = "خرید خدمات - استخدام / مشاوره / نمایندگی")]
        BuyServices,

        [EnumMember(Value = "قرارداد داوری")]
        [Display(Name = "قرارداد داوری")]
        Arbitration,

        [EnumMember(Value = "قراردادهای موضوع ماده 10 قانون مدنی")]
        [Display(Name = "قراردادهای موضوع ماده 10 قانون مدنی")]
        Article10,

        [EnumMember(Value = "سایر عقود نامعین")]
        [Display(Name = "سایر عقود نامعین")]
        OtherIndefiniteContracts,

        [EnumMember(Value = "متمم قرارداد - الحاقیه / اصلاحیه")]
        [Display(Name = "متمم قرارداد - الحاقیه / اصلاحیه")]
        ContractAmendment,

        [EnumMember(Value = "اقاله / تفاسخ قرارداد - انحلال دوطرفه")]
        [Display(Name = "اقاله / تفاسخ قرارداد - انحلال دوطرفه")]
        Recession,
    }
}
