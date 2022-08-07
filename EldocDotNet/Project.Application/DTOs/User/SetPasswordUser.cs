using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.User
{
    public class SetPasswordUser
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
