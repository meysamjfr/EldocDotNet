using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.User
{
    public class LoginUser
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Password { get; set; }
    }
}
