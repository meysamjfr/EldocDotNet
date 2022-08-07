using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum UserStatus
    {
        [EnumMember(Value = "درانتظار ثبت نام")]
        [Display(Name = "درانتظار ثبت نام")]
        Pending,
        [EnumMember(Value = "فعال")]
        [Display(Name = "فعال")]
        Active,
        [EnumMember(Value = "لغو دسترسی")]
        [Display(Name = "لغو دسترسی")]
        Banned,
    }
}
