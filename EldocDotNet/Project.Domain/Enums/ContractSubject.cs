using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum ContractSubject
    {
        [EnumMember(Value = "مال منقول")]
        [Display(Name = "مال منقول")]
        MovableProperty,

        [EnumMember(Value = "مال غیرمنقول")]
        [Display(Name = "مال غیرمنقول")]
        ImmovableProperty,

        [EnumMember(Value = "خدمات و فناوری")]
        [Display(Name = "خدمات و فناوری")]
        ServicesAndTechnology,

        [EnumMember(Value = "سایر حقوق")]
        [Display(Name = "سایر حقوق")]
        OtherRights,
    }
}
