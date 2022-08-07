using DNTPersianUtils.Core;

namespace Project.Application.DTOs.User
{
    public class SignupUser
    {
        [ValidIranianMobileNumber(ErrorMessage ="شماره همراه وارد شده معتبر نمی‌باشد")]
        public string Phone { get; set; }
    }
}
