using System.Runtime.Serialization;

namespace Project.Domain.Enums
{
    public enum UserStatus
    {
        [EnumMember(Value = "درانتظار ثبت نام")]
        Pending,
        [EnumMember(Value = "فعال")]
        Active,
        [EnumMember(Value = "لغو دسترسی")]
        Banned,
    }
}
