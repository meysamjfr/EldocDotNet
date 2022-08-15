using System.ComponentModel.DataAnnotations;

namespace Project.Application.DTOs.Expert
{
    public class ExpertLogin
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
