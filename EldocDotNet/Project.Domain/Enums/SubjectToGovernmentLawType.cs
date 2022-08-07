using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum SubjectToGovernmentLawType
    {
        [EnumMember(Value = "مالیات بر نقل و انتقال املاک")]
        [Display(Name = "مالیات بر نقل و انتقال املاک")]
        PropertyTransferTax,

        [EnumMember(Value = "مالیات بر نقل و انتقال خودرو و وسائط نقلیه")]
        [Display(Name = "مالیات بر نقل و انتقال خودرو و وسائط نقلیه")]
        VehicleTransferTax,

        [EnumMember(Value = "مالیات بر ارزش افزوده")]
        [Display(Name = "مالیات بر ارزش افزوده")]
        AddedValueTax,

        [EnumMember(Value = "مالیات بر درآمد مشاغل یا همان مالیات عملکرد")]
        [Display(Name = "مالیات بر درآمد مشاغل یا همان مالیات عملکرد")]
        BusinessIncomeTax,

        [EnumMember(Value = "مالیات بر درآمد املاک یا همان مالیات بر اجاره")]
        [Display(Name = "مالیات بر درآمد املاک یا همان مالیات بر اجاره")]
        PropertyIncomeTax,

        [EnumMember(Value = "مالیات بر ارث")]
        [Display(Name = "مالیات بر ارث")]
        InheritanceTax,

        [EnumMember(Value = "مالیات بر درآمدهای اتفاقی")]
        [Display(Name = "مالیات بر درآمدهای اتفاقی")]
        IncidentalIncomeTax,

        [EnumMember(Value = "سایر مالیات ها")]
        [Display(Name = "سایر مالیات ها")]
        OtherTax,

        [EnumMember(Value = "عوارض شهرداری تحت هر عنوان")]
        [Display(Name = "عوارض شهرداری تحت هر عنوان")]
        MunicipalFees,

        [EnumMember(Value = "دیون تامین اجتماعی")]
        [Display(Name = "دیون تامین اجتماعی")]
        SocialSecurityDebt,

        [EnumMember(Value = "دیون و هزینه های اجرایی و ثبتی تحت هر عنوان")]
        [Display(Name = "دیون و هزینه های اجرایی و ثبتی تحت هر عنوان")]
        ExecutiveAndRegistrationFeesDebt,

        [EnumMember(Value = "دیون صنف - اتحادیه و اتاق ها")]
        [Display(Name = "دیون صنف - اتحادیه و اتاق ها")]
        GuildDebt,

        [EnumMember(Value = "عوارض گمرکی و سود بازرگانی تحت هر عنوان")]
        [Display(Name = "عوارض گمرکی و سود بازرگانی تحت هر عنوان")]
        CustomsDuties,

        [EnumMember(Value = "سایر دیون و هزینه ها")]
        [Display(Name = "سایر دیون و هزینه ها")]
        OtherDebtAndFees,
    }
}
