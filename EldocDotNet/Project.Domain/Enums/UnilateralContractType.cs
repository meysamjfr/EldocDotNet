using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum UnilateralContractType
    {
        [EnumMember(Value = "اقرار")]
        [Display(Name = "اقرار")]
        Confession,

        [EnumMember(Value = "رضایت")]
        [Display(Name = "رضایت")]
        Assent,

        [EnumMember(Value = "تعهد")]
        [Display(Name = "تعهد")]
        Obligation,

        [EnumMember(Value = "وفای به عهد")]
        [Display(Name = "وفای به عهد")]
        FaithfulToTheCovenant,

        [EnumMember(Value = "تنفیذ")]
        [Display(Name = "تنفیذ")]
        Ratification,

        [EnumMember(Value = "استشهادیه - طلب شهادت")]
        [Display(Name = "استشهادیه - طلب شهادت")]
        Affidavit,

        [EnumMember(Value = "ابراء ذمه")]
        [Display(Name = "ابراء ذمه")]
        Discharge,

        [EnumMember(Value = "رجوع - از اذن یا حق")]
        [Display(Name = "رجوع - از اذن یا حق")]
        Reference,

        [EnumMember(Value = "بذل مدت - نکاح موقت")]
        [Display(Name = "بذل مدت - نکاح موقت")]
        Dispense,

        [EnumMember(Value = "فسخ قرارداد - انحلال یکطرفه")]
        [Display(Name = "فسخ قرارداد - انحلال یکطرفه")]
        Repudiation,
    }
}
